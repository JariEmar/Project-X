using Api.Contracts.v1;
using Api.Contracts.v1.Requests;
using Api.Contracts.v1.Responses;
using Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public IdentityController(IIdentityService identityService, IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authReponse = await identityService.RegisterAsync(request.Email, request.Password);

            if (authReponse.Success == false)
            {
                return BadRequest(
                    new AuthFailedResponse { Errors = authReponse.Errors }
                );
            }


            return Ok(new AuthSuccessReponse
            {
                Token = authReponse.Token
            });
        }
    }
}
