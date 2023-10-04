using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using apiSqlserver.Models.ModelsDto;
using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;


namespace apiSqlserver.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLibroController : ControllerBase
    {
        private readonly ITipoLibroRepositorio _tipolibrorrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public TipoLibroController(ITipoLibroRepositorio tipolibroRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _tipolibrorrepo = tipolibroRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaTipoLibro")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(401)]//no autentication
        public async Task<ActionResult<TipoLibroDto>> GetTipoLibro()
        {
            try
            {
                IEnumerable<TipoLibro> tipoautorlist = await _tipolibrorrepo.ListObjetos();
                return Ok(_mapper.Map<IEnumerable<TipoLibroDto>>(tipoautorlist));
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
