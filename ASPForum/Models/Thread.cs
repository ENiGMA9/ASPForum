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

        public Subject Subject;
        public virtual ICollection<Reply> Replies { get; set; }

        public ApplicationUser Author;
    }

    public class ThreadDBContext : DbContext
    {
        public ThreadDBContext() : base("DBConnectionString") { }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; }
    }

}