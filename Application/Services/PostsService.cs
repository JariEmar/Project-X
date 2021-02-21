using Application.Repositories;
using Application.Services.Interfaces;
using Domain.Common;
using Domain.Entities;
using LanguageExt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{

    public class PostsService : IPostsService
    {
        private readonly IPostsRepository postRepository;

        public PostsService(IPostsRepository postRepository)
        {
            this.postRepository = postRepository;
        }


        public async Task<List<Post>> GetPostsAsync(PaginationFilter paginationFilter)
        {
            return await postRepository.GetPostsAsync(paginationFilter);
        }


        public async Task<Option<Post>> GetPostByIdAsync(string postId)
        {
            return await postRepository.GetPostByIdAsync(postId);
        }


        public async Task<Option<Post>> CreatePostAsync(Post postToCreate)
        {
            return await postRepository.CreatePostAsync(postToCreate);
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            return await postRepository.UpdatePostAsync(postToUpdate);
        }

        public Post CreatePost()
        {
            return null;
        }

        public async Task<bool> DeletePostAsync(string postId)
        {
            return await postRepository.DeletePostAsync(postId);
        }

    }
}
