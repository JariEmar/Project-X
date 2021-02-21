using Api.Contracts.v1.Responses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(PaginationFilter paginationFilter, List<T> data, Func<PaginationFilter, Uri> getPageUri) 
        {
            var nextPage = data.Count == paginationFilter.PageSize
                ? getPageUri(new PaginationFilter(paginationFilter.PageNumber + 1, paginationFilter.PageSize))
                : null;

            var previousPage = paginationFilter.PageNumber - 1 >= 1
                ? getPageUri(new PaginationFilter(paginationFilter.PageNumber - 1, paginationFilter.PageSize))
                : null;

            var pagedResponse = new PagedResponse<T>
            {
                Data = data,
                PageSize = paginationFilter.PageSize,
                PageNumber = paginationFilter.PageNumber,
                NextPage = data.Any() ? nextPage : null,
                PreviousPage = previousPage
            };

            return pagedResponse;
        }
    }
}
