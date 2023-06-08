using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Social_Media.Models;
namespace Social_Media.ViewModel
{
    public class MessengerModel
    {
        public List<User> Users { get; set; }
        public List<Message> FirstOne { get; set; }
        public User LoggedUser { get; set; }
    }
}