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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [ApiController]
    [Produces("application/json")]
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

        /// <summary>
        /// Gets all posts
        /// </summary>
        /// <param name="paginationQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Cached(CacheTimeouts.Posts.GetAll)]
        [Route(ApiRoutes.Posts.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<PostResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Gets post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Posts.Get)]
        [Cached(CacheTimeouts.Posts.GetByGuid)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string postId)
        {
            var post = await postsService.GetPostByIdAsync(postId);

            return post.Match<IActionResult>(post =>
            {
                var result = mapper.Map<PostResponse>(post);
                return Ok(result);
            }, NotFound());
        }

        /// <summary>
        /// Creates a post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Posts.Create)]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostRequest request)
        {
            var post = mapper.Map<Post>(request);
            var result = await postsService.CreatePostAsync(post);

            return result.Match<IActionResult>(post =>
            {
                var postResponse = mapper.Map<PostResponse>(post);
                return Created(UriHelper.GetPostUri(post.Id), postResponse);
            }, BadRequest());
        }

        /// <summary>
        /// Updates a post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(ApiRoutes.Posts.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(ApiRoutes.Posts.Delete)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
