using System.IdentityModel.Tokens.Jwt;
using apiSqlserver.Models.ModelsDto;
using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace apiSqlserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuariorepo;
        private APIResponse _apiResponse;
        private string secretkey;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
        {
            _usuariorepo = usuarioRepositorio;
            _apiResponse = new();
            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        [HttpPost]
        [Route("/Register")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no conflict
        public async Task<ActionResult<APIResponse>> Register([FromBody] UsuarioCreatedDto modelCrt)
        {
            if (!ModelState.IsValid)
            {
                var message = "Campos Invalidos";
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Resultado = ModelState;
                _apiResponse.Alertmsg = message;
                return BadRequest(_apiResponse);
            }
                var reg = await _usuariorepo.Register(modelCrt);
            if(reg == null)
            {
                var message = "UserName ya existe";
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Resultado = ModelState;
                _apiResponse.Alertmsg = message;
                return BadRequest(_apiResponse);
            }
            _apiResponse.Alertmsg = "Usuario Creado Exitosamente";
            _apiResponse.Resultado = modelCrt;
            _apiResponse.StatusCode = HttpStatusCode.Created;
            return Ok(_apiResponse);
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto model)
        {
            var loginresponse = await _usuariorepo.Login(model);
            if (loginresponse.Usuario == null || string.IsNullOrEmpty(loginresponse.Token))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.Resultado = loginresponse;
                _apiResponse.ErrorMessage.Add("UserName o Password son Incorrectos");
                _apiResponse.Alertmsg = "UserName o Password son Incorrectos";
                return BadRequest(_apiResponse);
            }
            return Ok(loginresponse);
        }
        
        [HttpGet]
        [Route("/authStatus")]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(401)] // Unauthorized
        public async Task<IActionResult> CheckAuthStatus()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretkey);

                // Configura los parámetros de validación para el token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "InventarioBiblioteca",    // Emisor del token (nombre de tu backend)
                    ValidateAudience = true,
                    ValidAudience = "bookmanager-main", // Audiencia del token (nombre de tu frontend)
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromHours(8) // Sin margen de tiempo adicional
                };

                // Valida y decodifica el token
                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                var userNameClaim = claimsPrincipal.FindFirst(ClaimTypes.Name);
                var userTypes = claimsPrincipal.FindFirst(ClaimTypes.Sid);
                String userId="";
                String userName = "";
                String userType = "";
                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
                

                if (userNameClaim != null)
                {
                    userName = userNameClaim.Value;
                }

                if (userType != null)
                {
                    userType = userTypes.Value;
                }
                // Si la validación fue exitosa, el usuario está autenticado
                return Ok(new { ok = true, userId, userName, userType });
            }
            catch (Exception e)
            {
                // Si la validación falla, el usuario no está autenticado
                return Ok(new { ok = false });
            }
        }

        
        
    }
}
