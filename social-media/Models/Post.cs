using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public String Body { get; set; }



        public DateTime? DateTime { get; set; }

        public User User { get; set; }
        public byte UserID { get; set; }

        public Group Group { get; set; }
        public int? GroupID { get; set; }
        public int like_post { get; set; }
    }
}