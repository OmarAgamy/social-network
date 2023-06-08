using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Social_Media.Models
{
    public class RelationShip
    {
        [Key]





        public int RelationID { get; set; }
        public User Mate { get; set; }
        public User Friend { get; set; }
        public User ActionUser { get; set; }
        public int Status { get; set; }

    }
}