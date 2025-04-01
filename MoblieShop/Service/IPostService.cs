using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int postId);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
        Task HandleLikePostAsync(int postId, string userId);
        Task HandleDislikePostAsync(int postId, string userId);
        Task HandleRemoveLikeAsync(int postId, string userId);
        Task HandleRemoveDislikeAsync(int postId, string userId);
    }
}
