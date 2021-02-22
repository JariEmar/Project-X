using Api.Cache;
using Api.Contracts.v1;
using Api.Contracts.v1.Requests;
using Api.Contracts.v1.Requests.Queries;
using Api.Contracts.v1.Responses;
using Api.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService postsService;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public PostsController(
            IPostsService postsService, 
            IMapper mapper,
            ILogger logger)
        {
            this.postsService = postsService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Cached(CacheTimeouts.Posts.GetAll)]
        [Route(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationQuery paginationQuery, CancellationToken cancellationToken)
        {
            logger.Information("Just before validating model");
            if (!ModelState.IsValid) return BadRequest();
            logger.Information("Model validate: success");

            cancellationToken.ThrowIfCancellationRequested();
            logger.Warning("fieuw no cancellation requested");

            var paginationFilter = mapper.Map<PaginationFilter>(paginationQuery);

            var posts = await postsService.GetPostsAsync(paginationFilter);
            var postsResponse = mapper.Map<List<PostResponse>>(posts);

            var result = PaginationHelper.CreatePaginatedResponse(paginationFilter, postsResponse, UriHelper.GetAllPostsUri);

            return Ok(result);
        }

        [HttpGet]
        [Cached(CacheTimeouts.Posts.GetByGuid)]
        [Route(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string postId)
        {
            var post = await postsService.GetPostByIdAsync(postId);

            return post.Match<IActionResult>(post =>
            {
                var result = mapper.Map<PostResponse>(post);
                return Ok(result);
            }, NotFound());
        }

        [HttpPost]
        [Route(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostRequest request)
        {
            var post = mapper.Map<Post>(request);
            var result = await postsService.CreatePostAsync(post);

            return result.Match<IActionResult>(post =>
            {
                return Created(UriHelper.GetPostUri(post.Id), post);
            }, BadRequest());
        }

        [HttpPut]
        [Route(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] string postId, [FromBody] UpdatePostRequest request)
        {
            var post = mapper.Map<Post>(request);
            post.Id = postId;

            var isUpdated = await postsService.UpdatePostAsync(post);

            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpDelete]
        [Route(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] string postId)
        {
            var isDeleted = await postsService.DeletePostAsync(postId);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
