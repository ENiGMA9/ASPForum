using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ASPForum.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public int ThreadId { get; set; }

        public Thread Thread { get; set; }
    }
}