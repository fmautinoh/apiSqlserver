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
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoRepositorio _estadoRepositorio;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public EstadoController(IEstadoRepositorio estadoRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _estadoRepositorio = estadoRepositorio;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("/ListaEstado")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(401)]//no autentication
        public async  Task<ActionResult<EstadoDto>> GetListaEstado()
        {
            try
            {
                IEnumerable<EstadoConservacion> estado = await _estadoRepositorio.ListObjetos();
                return Ok(_mapper.Map<IEnumerable<EstadoDto>>(estado));
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
