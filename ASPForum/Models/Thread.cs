using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ASPForum.Models
{
    public class Thread
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject {get; set;}
        public virtual ICollection<Reply> Replies { get; set; }

        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }

}