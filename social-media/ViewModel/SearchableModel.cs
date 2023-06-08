using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Social_Media.Models;
namespace Social_Media.ViewModel
{
    public class SearchableModel
    {
        public User ResultUser { get; set; }
        public int RelationState { get; set; }
        public List<CommentsModel> FriendPosts { get; set; }
    }
}