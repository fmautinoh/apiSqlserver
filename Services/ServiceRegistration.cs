using apiSqlserver.Repositorio.IRepositorio.IReporteRepositorio;
using apiSqlserver.Repositorio.IRepositorio;
using apiSqlserver.Repositorio.ReporteRepositorio;
using apiSqlserver.Repositorio;

namespace apiSqlserver.Services
{
    public static class ServiceRegistration
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAutorRepositorio, AutorRepositorio>();
            services.AddScoped<ITipoAutorRepositorio, TipoAutorRepositorio>();
            services.AddScoped<ITipoLibroRepositorio, TipoLibroRepositorio>();
            services.AddScoped<ILibroRepositorio, LibroRepositorio>();
            services.AddScoped<ILibroxAutorRepositorio, LibroxAutorRepositorio>();
            services.AddScoped<Ivlibrorepositorio, vlibrorepositorio>();
            services.AddScoped<IvInventarioRepositorio, vInventarioRepositorio>();
            services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IEstadoRepositorio, EstadoRepositorio>();
            services.AddScoped<IAutenticidadRepositorio, AutenticidadRepositorio>();
            services.AddScoped<IAutorRepositorio, AutorRepositorio>();
            services.AddScoped<IvautorRepositorio, vAutorRepositorio>();
            services.AddScoped<IRepoteInventarioRepositorio, InvReporteRepositorio>();
        }
    }

}
