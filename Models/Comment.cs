using System;
using System.Collections.Generic;

namespace HackerNewsCloneApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } // Изменено на не-nullable строку
        public DateTime PostedOn { get; set; }

        // Foreign Keys
        public int PostId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}