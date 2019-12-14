using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ASPForum.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Index { get; set; }

        public int CategoryId;
        public Category Category;

        public virtual ICollection<Thread> Threads { get; set; }
        
    }

    public class SubjectDBContext : DbContext
    {
        public SubjectDBContext() : base("DBConnectionString") { }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Thread> Threads { get; set; }
    }

}