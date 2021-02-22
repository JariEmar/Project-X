using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Contracts.v1.Requests
{
    public class CreatePostRequest
    {
        public string Name { get; set; }

        public List<TagRequest> Tags { get; set; }

    }
}
