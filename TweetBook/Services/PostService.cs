using System;
using System.Collections.Generic;
using System.Linq;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService : IPostService
    {
        public PostService()
        {
            _posts = new List<Post>();

            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post()
                {
                    Id = Guid.NewGuid(),
                    Name = $"Post Name {i}"
                });
            }
        }
        private List<Post> _posts;

        public Post GetPostById(Guid postId)
        {
            return _posts.FirstOrDefault(x => x.Id == postId);
        }

        public List<Post> GetPosts()
        {
            return _posts;
        }

        public bool UpdatePost(Post postToUpdate)
        {
            var post = GetPostById(postToUpdate.Id);

            if(post == null) 
                return false;

            var index = _posts.FindIndex(x=>x.Id == postToUpdate.Id);
            _posts[index] = postToUpdate;

            return true;
        }
    }
}