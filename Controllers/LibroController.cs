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
    public class LibroController : ControllerBase
    {
        private readonly ILibroRepositorio _Librorrepo;
        private readonly Ivlibrorepositorio _vistalibrorepo;
        private readonly ILibroxAutorRepositorio _LibroAutorepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public LibroController(ILibroRepositorio libroRepositorio, Ivlibrorepositorio vistalibrorepo, ILibroxAutorRepositorio LibroAutorepo, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _Librorrepo = libroRepositorio;
            _vistalibrorepo = vistalibrorepo;
            _LibroAutorepo = LibroAutorepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaLibro")]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(400)] // BadRequest
        [ProducesResponseType(404)] // NotFound
        public async Task<ActionResult<VLibro>> GetLibro()
        {
            try
            {
                var listaLibros = await _vistalibrorepo.ListLibrosConAutores(); // Use the new method
                return Ok(listaLibros);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(new { _apiResponse });
        }
        
        [HttpPost]
        [Route("/CreateLibro")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found


        public async Task<ActionResult<VLibro>> CrearLibro([FromBody] LibroCreatedDto ModelLibro)
        {
            try
            {
                if (ModelLibro == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }

                var isexistente = await _Librorrepo.ListObjetos(c => c.NombreLib == ModelLibro.NombreLib);
                if (isexistente.Count != 0)
                {
                    var message = "Libro Existente";
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    _apiResponse.Alertmsg = message;
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

                var arreglolenght = ModelLibro.Autor.Count;

                Libro modelcreate = new()
                {
                    NombreLib = ModelLibro.NombreLib,
                    TipoId = ModelLibro.TipoId,
                    Edicion = ModelLibro.Edicion,
                    Año = ModelLibro.Año,
                    Editorial = ModelLibro.Editorial
                };
                await _Librorrepo.Crear(modelcreate);
                Libro creado = await _Librorrepo.Listar(c => c.NombreLib == ModelLibro.NombreLib, tracked: false);
                foreach (var autorId in ModelLibro.Autor)
                {
                    // Crear el registro en la tabla LibrosAutores
                    LibrosAutore modelautorlibro = new()
                    {
                        LibroId = creado.LibroId,
                        AutorId = autorId
                    };
                    await _LibroAutorepo.Crear(modelautorlibro);
                }

                var result =  await _vistalibrorepo.GetLibroConAutoresPorLibroid(creado.LibroId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
           return BadRequest(new { _apiResponse });
        }

        [HttpPut]
        [Route("/UpdateLibro/{idLib:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found

        public async Task<ActionResult<VLibro>> UpdateLibro(int idLib, [FromBody] LibroCreatedDto ModelLibro)
        {
            try
            {
                if (ModelLibro == null)
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

                var arreglolenght = ModelLibro.Autor.Count;

                Libro modelcreate = new Libro()
                {
                    LibroId = idLib,
                    NombreLib = ModelLibro.NombreLib,
                    TipoId = ModelLibro.TipoId,
                    Edicion = ModelLibro.Edicion,
                    Año = ModelLibro.Año,
                    Editorial = ModelLibro.Editorial
                };
                await _Librorrepo.Actualizar(modelcreate);
                Libro creado = await _Librorrepo.Listar(c => c.NombreLib == ModelLibro.NombreLib, tracked: false);
                List<LibrosAutore> listaLibroAutor = await _LibroAutorepo.ListObjetos(c => c.LibroId == idLib);

                // Update existing records with new author IDs
                foreach (var libroAutor in listaLibroAutor)
                {
                    int? autorId = ModelLibro.Autor.FirstOrDefault(a => a == libroAutor.AutorId);
                    if (autorId.HasValue)
                    {
                        libroAutor.AutorId = autorId.Value;
                        await _LibroAutorepo.Remover(libroAutor);
                    }
                }

                foreach (var autorId in ModelLibro.Autor)
                {
                    // Crear el registro en la tabla LibrosAutores
                    LibrosAutore modelautorlibro = new()
                    {
                        LibroId = idLib,
                        AutorId = autorId
                    };
                    LibrosAutore AutoreCrt = _mapper.Map<LibrosAutore>(modelautorlibro);
                    await _LibroAutorepo.Crear(modelautorlibro);
                }

                var result = await _vistalibrorepo.GetLibroConAutoresPorLibroid(idLib);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return BadRequest(_apiResponse);
        }



    }
}
