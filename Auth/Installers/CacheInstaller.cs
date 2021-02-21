using Api.Configurations;
using Application.Services;
using Application.Services.Interfaces;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Domain.Entities;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);

            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
            {
                return;
            }

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.AddMemoryCache();
        }


    }
}
