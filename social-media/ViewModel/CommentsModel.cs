using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Social_Media.Models;
namespace Social_Media.ViewModel
{
    public class CommentsModel
    {
        public Post post { get; set; }
        public List<Comment> Comments{ get; set; }
    }
}