using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPForum.Models
{
    public class Thread
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
       
        [Required]
        public virtual Subject Subject {get; set;}
        public virtual ICollection<Reply> Replies { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public Thread()
        {
            CreatedAt = DateTime.Now;
        }
    }

}