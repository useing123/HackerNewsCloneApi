using System.Collections.Generic;

namespace HackerNewsCloneApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Passwords should be stored hashed

        // Navigation properties
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
