using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Social_Media.Models;
using Social_Media.ViewModel;
namespace Social_Media.Controllers
{
    public class HomeController : Controller
    {
        MyDbcontext db = new MyDbcontext();
        public ActionResult Index()
        {
            var username = Session["Username"];
            User user = new User();
            user = db.Users.Where(u => u.Username == username).FirstOrDefault();
            var firstIDS = db.RelationShips.Where(x => x.Status == 3 && x.Friend.Username == username).Select(l => l.Mate).ToList();
            var secIDS = db.RelationShips.Where(x => x.Status == 3 && x.Mate.Username == username).Select(l => l.Friend).ToList();
            List<Post> fposts = new List<Post>();

            foreach (var data in secIDS)
                firstIDS.Add(data);
            foreach (var data in firstIDS)
            {
                var friendpost = db.Posts.Where(x => x.UserID == data.UserID).ToList();
                fposts.AddRange(friendpost);
            }

            var post = db.Posts.Where(x => x.UserID == user.UserID).ToList();
            List<CommentsModel> showposts = new List<CommentsModel>();
            foreach (var pos in post)
            {
                var commentss = db.Comment.Where(x => x.PostID == pos.ID).ToList();
                showposts.Add(new CommentsModel { Comments = commentss, post = pos });
            }

            List<CommentsModel> showfriendsposts = new List<CommentsModel>();
            foreach (var pos in fposts)
            {
                var commentss = db.Comment.Where(x => x.PostID == pos.ID).ToList();
                showfriendsposts.Add(new CommentsModel { Comments = commentss, post = pos });
            }
            ProfileModel profile = new ProfileModel { Friends = firstIDS, LoggedUser = user, Posts = showposts, FriendPosts = showfriendsposts };
            return View(profile);
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page. diaa ";

            return View();
        }
    }
}