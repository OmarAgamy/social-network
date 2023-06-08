using ServiceStack.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Social_Media.Models
{

    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "This filed is Required")]
        public String Username { get; set; }

        [Required(ErrorMessage = "This filed is Required")]
        public String Email { get; set; }
        public String Pic { get; set; }
        public string Cover { get; set; }
        public String Bio { get; set; }
        [Required(ErrorMessage = "This filed is Required")]

        [DataType(DataType.Password)]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public String Education { get; set; }
        public bool InActive { get; set; }
        public String Location { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}