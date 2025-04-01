using MoblieShop.ViewModels;
using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface ICommentService
    {
        Task AddCommentAsync(int postId, string content, string userId);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int id);
        Task<List<CommentViewModel>> GetMoreCommentsAsync(int postId, int offset, int count);
    }
}
