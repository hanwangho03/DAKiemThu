using MoblieShop.ViewModels;
using WebDoDienTu.Models;
using WebDoDienTu.Repository;

namespace MoblieShop.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(postId);
        }

        public async Task AddCommentAsync(int postId, string content, string userId)
        {
            var comment = new Comment
            {
                Content = content,
                CreatedAt = DateTime.UtcNow,
                AuthorId = userId,
                PostId = postId
            };

            await _commentRepository.AddCommentAsync(comment);
        }

        public async Task<List<CommentViewModel>> GetMoreCommentsAsync(int postId, int offset, int count)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            var moreComments = comments.Skip(offset).Take(count).ToList();

            return moreComments.Select(c => new CommentViewModel
            {
                AuthorName = c.Author.LastName,
                Content = c.Content,
                CreatedAt = c.CreatedAt.ToString("dd/MM/yyyy HH:mm")
            }).ToList();
        }
    }
}
