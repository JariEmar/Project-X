using Api.Contracts.v1;
using Api.Contracts.v1.Requests.Queries;
using Domain.Common;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public static class UriHelper
    {
        private readonly static string baseUri = "https://localhost:44313/";

        public static Uri GetAllPostsUri(PaginationFilter paginationFilter = null)
        {
            if(paginationFilter == null)
            {
                return new Uri(baseUri);
            }

            var modifiedUri = QueryHelpers.AddQueryString($"{baseUri}{ApiRoutes.Posts.GetAll}", "pageNumber", paginationFilter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationFilter.PageSize.ToString());

            return new Uri(modifiedUri);
        }

        public static Uri GetPostUri(string postId)
        {
            return new Uri($"{baseUri}{ApiRoutes.Posts.Get.Replace("{postId}", postId)}");
        }
    }
}
