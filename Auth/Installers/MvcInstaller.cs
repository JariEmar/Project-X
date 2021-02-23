using Api.Configurations;
using Api.Filters;
using Api.Mapping;
using Domain.Exceptions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            var jwtOptions = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtOptions);
            services.AddSingleton(jwtOptions);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false
                };
            });

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(RequestToDomainProfile));
            services.AddAutoMapper(typeof(DomainToResponseProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<OperationCancelledExceptionAttribute>();
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(configuration =>
            {
                configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

        }
    }
}
