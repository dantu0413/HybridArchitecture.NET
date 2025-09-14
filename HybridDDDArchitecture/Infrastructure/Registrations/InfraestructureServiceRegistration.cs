using Application.Repositories;
using Infrastructure.Factories;
using Infrastructure.Repositories.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Registrations
{
    public static class InfraestructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddDbContext<AutomovilDbContext>(o =>
                o.UseSqlServer(cfg.GetConnectionString("AutomovilDb")));

            services.AddScoped<IAutomovilRepository, AutomovilRepository>();
            // registrar tus handlers o el Bus en otro método

            return services;
        }
    }
}
