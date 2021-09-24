using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts;
using TweetBook.Contracts.v1.Requests;
using TweetBook.Contracts.v1.Responses;
using TweetBook.Domain;
using System.Linq;
using TweetBook.Services;
using System.Threading.Tasks;

namespace TweetBook.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }



        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post() { Name = postRequest.Name };

            if (post.Id != null)
                post.Id = Guid.NewGuid();

            await _postService.CreatePostAsync(post);

            var baseUrl = $"{Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString())}";

            var postResponse = new CreatePostResponse() { Id = post.Id, Name = post.Name };

            return Created(fullUrl, postResponse);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post()
            {
                Id = postId,
                Name = request.Name
            };

            var updated = await _postService.UpdatePostAsync(post);

            if (updated)
                return Ok();

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deleted = await _postService.DeletePostAsync(postId);

            if (deleted)
                return NoContent();
 
            return NotFound();
        }
    }


}
