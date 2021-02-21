using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Cache
{
    public static class CacheTimeouts
    {
        public static class Posts
        {
            public const int GetAll = 5;
            public const int GetByGuid = 5;
        }
    }
}
