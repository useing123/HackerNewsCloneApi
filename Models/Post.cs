using System;
using System.Collections.Generic;

namespace HackerNewsCloneApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}