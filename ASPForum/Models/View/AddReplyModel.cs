using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPForum.Models
{
    public class AddReplyModel
    {
        public int CategoryId { get; set; }
        public Thread Thread { get; set; }
        public Reply Reply { get; set; }
    }
}