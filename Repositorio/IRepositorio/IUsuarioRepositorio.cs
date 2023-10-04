using apiSqlserver.Models;
using apiSqlserver.Models.ModelsDto;

namespace apiSqlserver.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<LoginResponseDto> Login(UsuarioDto LgDto);
        Task<Usuario> Register(UsuarioCreatedDto UsuCrear);
    }
}
