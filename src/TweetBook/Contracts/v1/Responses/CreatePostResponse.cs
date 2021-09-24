using System;
namespace TweetBook.Contracts.v1.Responses
{
    public class CreatePostResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
