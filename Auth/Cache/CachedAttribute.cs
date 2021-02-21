using Api.Configurations;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api.Cache
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveMinutes;

        public CachedAttribute(int timeToLiveMinutes)
        {
            this.timeToLiveMinutes = timeToLiveMinutes;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                logger.Information("Cached result given back!!! :)", cacheResponse);
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromMinutes(timeToLiveMinutes));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
