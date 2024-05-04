using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HackerNewsCloneApi.Data;
using HackerNewsCloneApi.Models;
using System;
using System.Threading.Tasks;
using HackerNewsCloneApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization; // Добавить это

namespace HackerNewsCloneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly HackerNewsContext _context;

        public CommentController(HackerNewsContext context)
        {
            _context = context;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();
            return Ok(comments);
        }

        // POST: api/Comment
        [HttpPost]
        [Authorize] // Добавить эту атрибут
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto commentDto)
        {
            if (commentDto == null || string.IsNullOrWhiteSpace(commentDto.Text))
            {
                return BadRequest("Text field is required.");
            }

            // Получить имя пользователя из контекста аутентификации
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Identity.Name);
            if (currentUser == null)
            {
                return Unauthorized("User must be authenticated to create a comment.");
            }

            var post = await _context.Posts.FindAsync(commentDto.PostId);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            var comment = new Comment
            {
                Text = commentDto.Text,
                PostId = commentDto.PostId,
                UserId = currentUser.Id,
                PostedOn = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }


        // Additional method to support CreatedAtAction in CreateComment
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
    }
}
