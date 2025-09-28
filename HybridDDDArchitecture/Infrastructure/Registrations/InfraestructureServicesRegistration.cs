using Application.Repositories;
using Infrastructure.Factories;
using Infrastructure.Repositories.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Registrations
{
    /// <summary>
    /// Registro de servicios de Infraestructura:
    /// - DbContext (SQL Server)
    /// - Repositorios EF Core
    /// </summary>
    public static class InfraestructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Usa tu connection string "SqlConnection" desde appsettings
            services.AddDbContext<AutomovilDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            // Repositorio EF de Automóvil
            services.AddScoped<IAutomovilRepository, AutomovilRepository>();

            // No registramos adapters HTTP porque tu carpeta Adapters está vacía.
            // Si más adelante agregas uno, lo registras acá.

            return services;
        }
    }
}
