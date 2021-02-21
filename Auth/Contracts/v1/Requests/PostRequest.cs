using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Contracts.v1.Requests
{
    public class PostRequest
    {
        public Guid Id { get; set; }

        public List<TagRequest> Tags { get; set; }
    }
}
