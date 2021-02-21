using Api.Domain;
using System;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IResponseCacheService
    {

        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLiveInSeconds);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
