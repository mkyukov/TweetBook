using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts;
using TweetBook.Contracts.v1.Requests;
using TweetBook.Contracts.v1.Responses;
using TweetBook.Domain;

namespace TweetBook.Controllers
{
    public class PostsController : Controller
    {
        private List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();

            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post() { Id = Guid.NewGuid().ToString()});
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post() { Id = postRequest.Id };

            if (string.IsNullOrEmpty(post.Id))
                post.Id = Guid.NewGuid().ToString();

            _posts.Add(post);

            var baseUrl = $"{Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}",post.Id)}";

            var postResponse = new CreatePostResponse() { Id = post.Id };

            return Created(fullUrl, postResponse);
        }
    }

    
}
 