using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HackerNewsCloneApi.Data;
using HackerNewsCloneApi.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly HackerNewsContext _context;

    public UserController(HackerNewsContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // Additional method to support CreatedAtAction in CreateUser
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
}
