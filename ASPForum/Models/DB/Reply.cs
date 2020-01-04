using System;
using System.ComponentModel.DataAnnotations;

namespace ASPForum.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public virtual Thread Thread { get; set; }

        public DateTime CreatedAt { get; set; }

        public Reply()
        {
            CreatedAt = DateTime.Now;
        }
    }
}