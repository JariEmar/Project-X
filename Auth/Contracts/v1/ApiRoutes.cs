using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Contracts.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Posts
        {
            public const string GetAll = Base + "/posts";
            public const string Get = Base + "/{postId}";
            public const string Create = Base + "/posts";
            public const string Update = Base + "/{postId}";
            public const string Delete = Base + "/{postId}";
        }

        public static class Identity
        {
            public const string Login = Base + "/login";
            public const string Register = Base + "/register";
        }
    }
}
