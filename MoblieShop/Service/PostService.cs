using Microsoft.AspNetCore.SignalR;
using WebDoDienTu.Hubs;
using WebDoDienTu.Models;
using WebDoDienTu.Repository;

namespace MoblieShop.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IHubContext<PostHub> _hubContext;

        public PostService(IPostRepository postRepository, IHubContext<PostHub> hubContext)
        {
            _postRepository = postRepository;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            return await _postRepository.GetPostByIdAsync(postId);
        }

        public async Task AddPostAsync(Post post)
        {
            await _postRepository.AddPostAsync(post);
        }

        public async Task UpdatePostAsync(Post post)
        {
            await _postRepository.UpdatePostAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeletePostAsync(id);
        }

        public async Task HandleLikePostAsync(int postId, string userId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) return;

            await _postRepository.LikePostAsync(post, userId);
            await UpdateLikes(post);
        }

        public async Task HandleDislikePostAsync(int postId, string userId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) return;

            await _postRepository.DisLikePostAsync(post, userId);
            await UpdateLikes(post);
        }

        public async Task HandleRemoveLikeAsync(int postId, string userId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) return;

            await _postRepository.RemoveLikeAsync(post, userId);
            await UpdateLikes(post);
        }

        public async Task HandleRemoveDislikeAsync(int postId, string userId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) return;

            await _postRepository.RemoveDislikeAsync(post, userId);
            await UpdateLikes(post);
        }

        private async Task UpdateLikes(Post post)
        {
            int totalLikes = post.ActionPosts.Count(ap => ap.Like);
            int totalDislikes = post.ActionPosts.Count(ap => ap.Dislike);

            await _hubContext.Clients.All.SendAsync("UpdateLikes", post.PostId, totalLikes, totalDislikes);
        }
    }
}
