using AutoMapper;
using apiSqlserver.Models.ModelsDto;
using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace apiSqlserver.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IvInventarioRepositorio _vInvenrepo;
        private readonly IInventarioRepositorio _Invrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;
        public InventarioController(IInventarioRepositorio inventarioRepositorio, IvInventarioRepositorio vistarepo, IMapper mapper)
        {
            _vInvenrepo = vistarepo;
            _apiResponse = new APIResponse();
            _mapper = mapper;
            _Invrepo = inventarioRepositorio;
        }

        [HttpGet]
        [Route("/ListaInventario")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<VInventario>> GetInventario()
        {
            try
            {
                IEnumerable<VInventario> Invlist = await _vInvenrepo.ListObjetos();
                return Ok(Invlist);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }

        [HttpGet]
        [Route("/ListaInventario/{idInv:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<VInventario>> GetAutorporID(int idInv)
        {
            try
            {
                if (idInv == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }
                var inven = await _vInvenrepo.ListObjetos(c => c.InventarioId == idInv);
                if (inven == null)
                {
                    _apiResponse.Alertmsg = "Libro no encontrado no Encontrado";
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccess = false;

                    return NotFound(_apiResponse);
                }
                return Ok(inven);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }

        [HttpPost]
        [Route("/CreateInventario")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found

        public async Task<ActionResult<VInventario>> CreateInventario([FromBody] InventarioDto ModelInv)
        {
            try
            {
                if (ModelInv == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }


                if (!ModelState.IsValid)
                {
                    var message = "Campos Invalidos";
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    _apiResponse.Resultado = ModelState;
                    _apiResponse.Alertmsg = message;
                    return BadRequest(_apiResponse);
                }

                InventarioLibro InventCrt = _mapper.Map<InventarioLibro>(ModelInv);
                await _Invrepo.Crear(InventCrt);
                var creado = await _vInvenrepo.ObtenerPrimerElementoDescendente(ordenarPor: x => x.InventarioId);
                return Ok(creado);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }

        [HttpPut]
        [Route("/updateInventario/{idInv:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content

        public async Task<ActionResult<VInventario>> UpdateInventario(int idInv, [FromBody] InventarioDto ModelInv)
        {
            try
            {
                if (idInv == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    return BadRequest(_apiResponse);
                }

                InventarioLibro mdInvUp = new()
                {
                    InventarioId = idInv,
                    LibroId = ModelInv.LibroId,
                    Codigo = ModelInv.Codigo,
                    EstadoId = ModelInv.EstadoId,
                    AutenticidadId = ModelInv.Autenticidadid
                };

                await _Invrepo.Actualizar(mdInvUp);
                var resultado = await _vInvenrepo.Listar(c => c.InventarioId == idInv, tracked: false);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { e.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }

        [HttpDelete]
        [Route("/deleteLibros/{idlib:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content

        public async Task<IActionResult> DeleteLib(int idlib)
        {
            try
            {
                var MdLibro = await _Invrepo.Listar(c => c.InventarioId == idlib, tracked: false);

                InventarioLibro deleteLB = new()
                {
                    InventarioId = idlib,
                    Codigo = MdLibro.Codigo,
                    EstadoId = MdLibro.EstadoId,
                    LibroId = MdLibro.LibroId,
                    AutenticidadId = MdLibro.AutenticidadId
                };

                await _Invrepo.Remover(deleteLB);
                return Ok();
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return Ok(_apiResponse);
        }


    }
}
