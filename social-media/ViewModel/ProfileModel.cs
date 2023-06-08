using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Social_Media.Models;
namespace Social_Media.ViewModel
{
    public class ProfileModel
    {
        public User LoggedUser { get; set; }
        public List<User> Friends { get; set; }
        public List<CommentsModel> Posts { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword")]
        public string CurrentPassword { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "RepeatPassword")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public List<CommentsModel> FriendPosts { get; set; }
}
}