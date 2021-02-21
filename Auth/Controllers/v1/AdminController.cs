
using Api.Contracts.v1;
using Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IResponseCacheService responseCacheService;
        private readonly IMapper mapper;


        public AdminController(IResponseCacheService responseCacheService, IMapper mapper)
        {
            this.responseCacheService = responseCacheService;
            this.mapper = mapper;

        }

    }
}
