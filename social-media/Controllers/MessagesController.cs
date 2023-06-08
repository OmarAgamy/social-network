using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Social_Media.Models;
using Social_Media.ViewModel;


namespace Social_Media.Controllers
{
    public class MessagesController : Controller
    {
        MyDbcontext db = new MyDbcontext();
        public ActionResult Index()
        {
            String namee = (String)Session["Username"];
            User login = db.Users.Where(x => x.Username.Equals(namee)).FirstOrDefault();
            ViewBag.login = login.UserID;
            MessengerModel messenger = new MessengerModel();
            messenger.LoggedUser = login;
            messenger.Users = Friends(login);
            return View(messenger);
        }
        public List<User> Friends(User login)
        {
            var friends1 = db.RelationShips.Where(x => (x.Mate.UserID == login.UserID) && x.Status == 3).Select(l => l.Friend).ToList();
            var friends2 = db.RelationShips.Where(x => (x.Friend.UserID == login.UserID) && x.Status == 3).Select(l => l.Mate).ToList();
            foreach (var friend in friends2)
            {
                friends1.Add(friend);
            }
            return friends1;
        }
        public JsonResult chatdata(int friendID)
        {
            String namee = (String)Session["Username"];
            User login = db.Users.Where(x => x.Username.Equals(namee)).FirstOrDefault();

            User friend = db.Users.Where(x => x.UserID == friendID).Single();
            var data = db.Messages.Where(x => (x.Receiver.UserID == login.UserID && x.Sender.UserID == friendID) || (x.Receiver.UserID == friendID && x.Sender.UserID == login.UserID))
                .Select(
                x => new
                {
                    Content = x.Content,
                    Seen = x.Seen,
                    SenderID = x.Sender.UserID,
                    ReceiverID = x.Receiver.UserID,
                    Time = x.Time.ToString(),
                    ReceiverPic = x.Receiver.Pic,
                    SenderPic = x.Sender.Pic,

                }

                ).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public String SendMessage(String Content, int friendID)
        {
            String namee = (String)Session["Username"];
            User login = db.Users.Where(x => x.Username.Equals(namee)).FirstOrDefault();

            User sender = db.Users.Where(x => x.UserID == login.UserID).Single();
            User recevier = db.Users.Where(x => x.UserID == friendID).Single();
            DateTime MsgTime = DateTime.Now;
            db.Messages.Add(new Message { Content = Content, Seen = false, Time = MsgTime, Receiver = recevier, Sender = sender });
            db.SaveChanges();
            return MsgTime.ToString();
        }
    }
}