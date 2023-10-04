using apiSqlserver.Models.ModelsDto;
using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio.IReporteRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiSqlserver.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IRepoteInventarioRepositorio _repoteInventarioRepositorio;
        protected APIResponse _apiResponse;

        public ReporteController(IRepoteInventarioRepositorio repoteInventarioRepositorio)
        {
            _apiResponse = new APIResponse();
            _repoteInventarioRepositorio=repoteInventarioRepositorio;
        }
        [HttpGet]
        [Route("/ReporteInventarioGeneral")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<VInvReporte>> ReporteInventario()
        {
            try
            {
                var vInv = await _repoteInventarioRepositorio.ListObjetos();
                return Ok(vInv);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }

        [HttpGet]
        [Route("/ReporteInventarioEspecifico/{idlib:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<VInvReporte>> ReporteInventarioID(int idlib)
        {
            try
            {
                var vInv = await _repoteInventarioRepositorio.ListObjetos(x=>x.LibroId == idlib);
                return Ok(vInv);
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
