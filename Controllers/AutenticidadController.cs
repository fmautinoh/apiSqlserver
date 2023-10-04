using AutoMapper;
using apiSqlserver.Models.ModelsDto;
using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using apiSqlserver.Models.ModelsDto;
using Microsoft.AspNetCore.Authorization;

namespace apiSqlserver.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticidadController : ControllerBase
    {
        private readonly IAutenticidadRepositorio _autenticidadRepositorio;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public AutenticidadController(IAutenticidadRepositorio autenticidadRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _autenticidadRepositorio = autenticidadRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaAutenticidad")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<AutenticidadDto>> GetTipoAutor()
        {
            try
            {
                IEnumerable<Autenticidad> autenticidad = await _autenticidadRepositorio.ListObjetos();
                return Ok(_mapper.Map<IEnumerable<AutenticidadDto>>(autenticidad));
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
