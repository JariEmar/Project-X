﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Contracts.v1.Requests
{
    public class TagRequest
    {
        public string Name { get; set; }
        public string Color { get; set; }

    }
}
