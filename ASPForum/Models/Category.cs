using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ASPForum.Models {
    public class Category {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Index { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }

    public class CategoryDBContext : DbContext {
        public CategoryDBContext() : base("DBConnectionString") { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }

}