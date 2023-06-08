using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Social_Media.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public String Content { get; set; }
        public User User { get; set; }
        public byte UserID { get; set; }

        public Post Post { get; set; }
        public byte PostID { get; set; }
    }
}