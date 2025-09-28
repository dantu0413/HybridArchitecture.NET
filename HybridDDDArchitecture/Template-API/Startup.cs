using Application.UseCases.Automoviles.Handlers;
using Infrastructure.Registrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Filters;
using System;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Este método se llama en runtime. Acá registramos dependencias (DI).
        public void ConfigureServices(IServiceCollection services)
        {
            // Controllers y Swagger (útil para probar los endpoints del parcial)
            services.AddControllers(options => {options.Filters.Add<HttpExceptionFilter>();});
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // ---------- Infraestructura ----------
            // Registramos DbContext + Repos + Adapters usando nuestra extensión.
            // Nota: Por defecto, AddInfrastructureServices usa:
            // configuration.GetConnectionString("AutomovilDb")
            // Para alinear con tu appsettings (SqlConnection), ajustamos con un "fallback":
            services.AddInfrastructureServices(new ConnectionStringOverride(Configuration, "SqlConnection"));

            // ---------- Handlers de Automóviles ----------
            services.AddScoped<CreateAutomovilHandler>();
            services.AddScoped<UpdateAutomovilHandler>();
            services.AddScoped<DeleteAutomovilHandler>();
            services.AddScoped<GetAutomovilByIdHandler>();
            services.AddScoped<GetAutomovilByChasisHandler>();
            services.AddScoped<GetAllAutomovilesHandler>();
        }

        // Este método configura la tubería HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    /// <summary>
    /// Pequeño helper para permitir que AddInfrastructureServices tome "SqlConnection"
    /// en vez de "AutomovilDb" sin tocar tu Infra. Si preferís, más tarde cambiamos
    /// la extensión para leer "SqlConnection" directamente.
    /// </summary>
    internal class ConnectionStringOverride : IConfiguration
    {
        private readonly IConfiguration _inner;
        private readonly string _overrideName;

        public ConnectionStringOverride(IConfiguration inner, string overrideName)
        {
            _inner = inner;
            _overrideName = overrideName;
        }

        public string this[string key]
        {
            get => _inner[key];
            set => _inner[key] = value;
        }

        public IEnumerable<IConfigurationSection> GetChildren() => _inner.GetChildren();
        public IChangeToken GetReloadToken() => _inner.GetReloadToken();
        public IConfigurationSection GetSection(string key)
        {
            // Si Infra pide "ConnectionStrings:AutomovilDb", devolvemos la sección de "SqlConnection"
            if (key == "ConnectionStrings:AutomovilDb")
            {
                return new RedirectSection(_inner.GetSection("ConnectionStrings"), "AutomovilDb", _overrideName);
            }
            return _inner.GetSection(key);
        }

        private class RedirectSection : IConfigurationSection
        {
            private readonly IConfigurationSection _connRoot;
            private readonly string _expected;
            private readonly string _actual;

            public RedirectSection(IConfigurationSection connRoot, string expected, string actual)
            {
                _connRoot = connRoot;
                _expected = expected;
                _actual = actual;
            }

            public string this[string key]
            {
                get => _connRoot[key];
                set => _connRoot[key] = value;
            }

            public string Key => _expected;
            public string Path => _connRoot.Path + ":" + _expected;
            public string Value
            {
                get => _connRoot[_actual];
                set => _connRoot[_actual] = value;
            }

            public IEnumerable<IConfigurationSection> GetChildren() => _connRoot.GetChildren();
            public IChangeToken GetReloadToken() => _connRoot.GetReloadToken();
            public IConfigurationSection GetSection(string key) => _connRoot.GetSection(key);
        }
    }
}
