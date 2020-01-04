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

        [Required]
        public virtual Category Category { get; set; }

        public virtual ICollection<Thread> Threads { get; set; }
        
    }

}