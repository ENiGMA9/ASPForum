using System;
using System.ComponentModel.DataAnnotations;

namespace ASPForum.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public int ThreadId { get; set; }

        public Thread Thread { get; set; }

        public DateTime CreatedAt { get; set; }

        public Reply()
        {
            CreatedAt = DateTime.Now;
        }
    }
}