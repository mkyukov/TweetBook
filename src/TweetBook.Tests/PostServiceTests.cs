using Xunit;
using TweetBook.Services;
using System;
using TweetBook.Data;
using TweetBook.Domain;
using System.Threading.Tasks;
using System.Linq;

namespace TweetBook.Tests
{
    public class PostServiceTests
    {
        [Fact]
        public async Task CanInsertPost()
        {
            await WithTestDatabase.Run(async (DataContext context) => {
                var postName = "MyTestPost";
                var postService = new PostService(context);
                var createdPost = await postService.CreatePostAsync(new Post() { Name = postName});

                Assert.True(createdPost);
            });
        }

        [Fact]
        public async Task InMemDbSeeded()
        {
            await WithTestDatabase.Run(async (DataContext context) => {
                var postService = new PostService(context);
                var postsFromDb = await postService.GetPostsAsync();
                
                Assert.NotEmpty(postsFromDb);
            });
        }

        [Fact]
        public async Task CanGet()
        {
            await WithTestDatabase.Run(async (DataContext context) => {
                var postName = "Test post #0";
                var postService = new PostService(context);
                var postsFromDb = await postService.GetPostsAsync();
                var postFromDb = postsFromDb.FirstOrDefault(x=>x.Name.Equals(postName));

                Assert.NotNull(postFromDb);
            });
        }
    }

}