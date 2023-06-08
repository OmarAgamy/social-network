using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Social_Media.Models
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        public String GroupName { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public String Description { get; set; }

    }
}