using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HackerNewsCloneApi.Data;
using HackerNewsCloneApi.Models;
using HackerNewsCloneApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsCloneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly HackerNewsContext _context;

        public PostController(HackerNewsContext context)
        {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.Posts.Include(p => p.User).Include(p => p.Comments).ToListAsync();
            return Ok(posts);
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _context.Posts.Include(p => p.User).Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // POST: api/Post
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto postDto)
        {
            if (postDto == null || string.IsNullOrWhiteSpace(postDto.Title) || string.IsNullOrWhiteSpace(postDto.Content))
            {
                return BadRequest("Title and Content fields are required.");
            }

            // Assuming UserId is passed in the request or you may fetch it from the current user's identity
            // Here, I assume UserId is passed in the postDto
            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                PostedOn = DateTime.UtcNow,
                UserId = postDto.UserId // Assuming UserId is passed in the request
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }
    }
}
