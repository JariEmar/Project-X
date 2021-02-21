using Application.Repositories;
using Cosmonaut;
using Cosmonaut.Extensions;
using Domain.Common;
using Domain.Entities;
using LanguageExt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ICosmosStore<Post> cosmosStore;
        public PostsRepository(ICosmosStore<Post> cosmosStore)
        {
            this.cosmosStore = cosmosStore;
        }

        public async Task<List<Post>> GetPostsAsync(PaginationFilter paginationFilter)
        {
            var posts = await cosmosStore.Query().WithPagination(paginationFilter.PageNumber, paginationFilter.PageSize).ToListAsync();
            return posts;
        }

        public async Task<Option<Post>> GetPostByIdAsync(string postId)
        {
            var post = await cosmosStore.FindAsync(postId.ToString(), postId.ToString());

            return post;
        }

        public async Task<Option<Post>> CreatePostAsync(Post postToCreate)
        {
            await cosmosStore.AddAsync(postToCreate);

            return postToCreate;
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            var response = await cosmosStore.UpdateAsync(postToUpdate);

            return response.IsSuccess;
        }

        public async Task<bool> DeletePostAsync(string postId)
        {
            var response = await cosmosStore.RemoveByIdAsync(postId);

            return response.IsSuccess;
        }
    }
}
