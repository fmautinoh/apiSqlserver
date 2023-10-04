using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using apiSqlserver.Repositorio.IRepositorio;
using apiSqlserver.Models.ModelsDto;
using apiSqlserver.Models;

namespace apiSqlserver.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAutorController : ControllerBase
    {
        private readonly ITipoAutorRepositorio _tipoautorrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public TipoAutorController(ITipoAutorRepositorio tipoautorRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _tipoautorrepo = tipoautorRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaTipoAutores")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<TipoAutorDto>> GetTipoAutor()
        {
            try
            {
                IEnumerable<TipoAutor> tipoautorlist = await _tipoautorrepo.ListObjetos();
                var resultado = _mapper.Map<IEnumerable<TipoAutorDto>>(tipoautorlist);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }
    }
}