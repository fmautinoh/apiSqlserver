using apiSqlserver.Models;
using apiSqlserver.Models.ModelsDto;
using AutoMapper;

namespace apiSqlserver
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Autore, AutorDto>().ReverseMap();
            CreateMap<Autore, AutorCreatedDto>().ReverseMap();
            CreateMap<LibroDto, VLibro>().ReverseMap();
            CreateMap<TipoAutorDto, TipoAutor>().ReverseMap();
            CreateMap<TipoLibroDto, TipoLibro>().ReverseMap();
            CreateMap<LibrosAutore, LibroAutorCreatedDto>().ReverseMap();
            CreateMap<InventarioLibro, InventarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioCreatedDto>().ReverseMap();
            CreateMap<EstadoConservacion, EstadoDto>().ReverseMap();
            CreateMap<Autenticidad, AutenticidadDto>().ReverseMap();



        }
    }
}
