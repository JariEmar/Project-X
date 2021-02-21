using Api.Domain;
using Application.Configurations;
using Application.Repositories;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLiveInSeconds)
        {
            if(response == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(response);

            await distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLiveInSeconds
            });
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cacheResponse = await distributedCache.GetStringAsync(cacheKey);

            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }
    }
}
