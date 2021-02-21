using Application.Repositories;
using Application.Services;
using Application.Services.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = @"Server=.\SQLEXPRESS;Database=Auth;Integrated Security=true";

            //services.AddDbContext<DataContext>(
            //    options => options.UseSqlServer(connectionString));


            //services.AddScoped<DbContext, DataContext>();
            services.AddTransient<IPostsRepository, PostsRepository>();
            services.AddTransient<IPostsService, PostsService>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<IIdentityRepository, IdentityRepository>();

        }
    }
}
