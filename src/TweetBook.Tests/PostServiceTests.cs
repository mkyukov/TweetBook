using Xunit;
using TweetBook.Services;
using System;
using TweetBook.Data;
using TweetBook.Domain;
using System.Threading.Tasks;

namespace TweetBook.Tests
{
    public class PostServiceTests
    {
        [Fact]
        public async Task CanAddPost()
        {
            await WithTestDatabase.Run(async (DataContext context) => {
                var postService = new PostService(context);
                var createdPost = await postService.CreatePostAsync(new Post() { Name="MyTestPost" });

                Assert.True(createdPost);
            });
        }
    }

}