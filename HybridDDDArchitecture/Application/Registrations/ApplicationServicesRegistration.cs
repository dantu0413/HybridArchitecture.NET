using Microsoft.Extensions.DependencyInjection;

namespace Application.Registrations
{
    /// <summary>
    /// Registro de servicios de la capa Application.
    /// En este parcial no registramos servicios específicos aquí.
    /// </summary>
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Si más adelante agregás validadores/mediators/etc., registralos aquí.
            return services;
        }
    }
}