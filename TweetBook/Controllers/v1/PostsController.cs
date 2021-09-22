﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts;
using TweetBook.Contracts.v1.Requests;
using TweetBook.Contracts.v1.Responses;
using TweetBook.Domain;
using System.Linq;
using TweetBook.Services;

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
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var post = _postService.GetPostById(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post()
            {
                Id = postId,
                Name = request.Name
            };

            var updated = _postService.UpdatePost(post);

            if (updated)
                return Ok();

            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post() { Id = postRequest.Id };

            if (post.Id != null)
                post.Id = Guid.NewGuid();

            _postService.GetPosts().Add(post);

            var baseUrl = $"{Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString())}";

            var postResponse = new CreatePostResponse() { Id = post.Id, Name = post.Name };

            return Created(fullUrl, postResponse);
        }
    }


}
