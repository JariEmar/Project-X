using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Contracts.v1.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; }

        public ErrorResponse()
        {
            Errors  = new List<ErrorModel>();
        }
    }
}
