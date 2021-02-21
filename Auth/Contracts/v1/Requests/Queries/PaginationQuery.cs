using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Contracts.v1.Requests.Queries
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 100;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = Math.Clamp(pageNumber, 1, int.MaxValue);
            PageSize = Math.Clamp(pageSize, 1, 100);
        }

        public int PageNumber { get; set; }

        [Range(1, 100)]
        public int PageSize { get; set; }
    }
}
