using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Social_Media.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public String Content { get; set; }
        public Boolean Seen { get; set; }
        public DateTime Time { get; set; }
    }
}