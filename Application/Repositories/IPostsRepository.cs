using Domain.Common;
using Domain.Entities;
using LanguageExt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IPostsRepository
    {
        Task<List<Post>> GetPostsAsync(PaginationFilter paginationFilter);

        Task<Option<Post>> GetPostByIdAsync(string postId);

        Task<Option<Post>> CreatePostAsync(Post postToCreate);

        Task<bool> UpdatePostAsync(Post postToUpdate);

        Task<bool> DeletePostAsync(string postId);

    }
}
