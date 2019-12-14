using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ASPForum.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public ApplicationUser Author;
    }

    public class ReplyDBContext : DbContext
    {
        public ReplyDBContext() : base("DBConnectionString") { }
        public DbSet<Reply> Replies { get; set; }
    }

}