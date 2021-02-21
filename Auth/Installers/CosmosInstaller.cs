using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Domain.Entities;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers
{
    public class CosmosInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var cosmosStoreSettings = new CosmosStoreSettings(
                configuration["CosmosSettings:DatabaseName"],
                configuration["CosmosSettings:AccountUri"],
                configuration["CosmosSettings:AccountKey"],
                new ConnectionPolicy {  ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp}
                );

            services.AddCosmosStore<Post>(cosmosStoreSettings);
        }

    }
}
