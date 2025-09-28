using Application.Repositories;
// Si usás el adapter HTTP del template:
using Core.Application; // IExternalApiClient (interfaz)
using Core.Infraestructure.Adapters.Http; // ExternalApiHttpAdapter (impl)
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
    /// - Adapters (HTTP, etc.)
    /// </summary>
    public static class InfraestructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1) DbContext: SQL Server Local
            // Asegurate de tener ConnectionStrings:AutomovilDb en appsettings.json
            services.AddDbContext<AutomovilDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AutomovilDb")));

            // 2) Repositorios (EF)
            services.AddScoped<IAutomovilRepository, AutomovilRepository>();

            // 3) Adapters opcionales (del template)
            services.AddSingleton<IExternalApiClient, ExternalApiHttpAdapter>();

            // (Si tenés mensajería/EventBus, registralo aquí también)

            return services;
        }
    }
}
