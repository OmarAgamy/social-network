using System.Linq;
using System.Web;
using System.IO;
using Social_Media.Models;
using Social_Media.ViewModel;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Collections.Generic;

namespace Social_Media.Controllers
{
    public class ProfileController : Controller
    {
        private MyDbcontext db = new MyDbcontext();
        public ActionResult ProfileTemp()
        {

            var username = Session["Username"];
            User user = new User();
          
            user = db.Users.Where(u => u.Username == username).FirstOrDefault();
            var post = db.Posts.Where(x => x.UserID == user.UserID).ToList();
            List<CommentsModel> showposts = new List<CommentsModel>();
            var firstIDS = db.RelationShips.Where(x => x.Status == 3 && x.Friend.Username == username).Select(l => l.Mate).ToList();
            var secIDS = db.RelationShips.Where(x => x.Status == 3 && x.Mate.Username == username).Select(l => l.Friend).ToList();
            foreach (var data in secIDS)
                firstIDS.Add(data);
           foreach(var pos in post)
            {
                var commentss = db.Comment.Where(x => x.PostID == pos.ID).ToList();
                showposts.Add(new CommentsModel { Comments = commentss, post = pos });
            }
            ProfileModel profile = new ProfileModel { Friends = firstIDS, LoggedUser = user, Posts = showposts };
            return View(profile);
        }
        // GET: Profile
        [HttpPost]
        public ActionResult UploadPic(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var username = Session["Username"];
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), file.FileName);
                    file.SaveAs(path);
                    db.Users.FirstOrDefault(u => u.Username == username).Pic = file.FileName;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ProfileTemp");
        }
        [HttpPost]
        public ActionResult UploadCover(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), file.FileName);
                    file.SaveAs(path);
                    var username = Session["Username"];
                    db.Users.FirstOrDefault(u => u.Username == username).Cover = file.FileName;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ProfileTemp");
        }
        public void Block(int friendID)
        {
            var username = Session["Username"];
            int Relation = db.RelationShips.Where(x => ((x.Mate.UserID == friendID) && (x.Friend.Username == username)) || ((x.Friend.UserID == friendID) && (x.Mate.Username == username)))
                       .Select(l => l.RelationID).SingleOrDefault();
            RelationShip update = new RelationShip();
            update = db.RelationShips.Where(x => x.RelationID == Relation).Single();
            update.Status = 2;
            db.RelationShips.Attach(update);
            db.Entry(update).Property(x => x.Status).IsModified = true;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
        }
        public ActionResult MoveToEditProfile()
        {
            return RedirectToAction("EditProfile");
        }
        public ActionResult EditProfile(ProfileModel profile)
        {

            return View(profile);
        }
        [HttpPost]
        public ActionResult EditInfo(Social_Media.Models.User userinfo)
        {
            if (ModelState.IsValid)
            {
                var username = Session["Username"];
                db.Users.FirstOrDefault(u => u.Username == username).Bio = userinfo.Bio;
                db.Users.FirstOrDefault(u => u.Username == username).Education = userinfo.Education;
                db.Users.FirstOrDefault(u => u.Username == username).Location = userinfo.Location;
                db.Users.FirstOrDefault(u => u.Username == username).Birthdate = userinfo.Birthdate;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                ViewBag.Message = "Info has been changed";

            }
            return RedirectToAction("ProfileTemp");
        }
        [HttpPost]
        public ActionResult ChangePassword(ProfileModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                var username = Session["Username"];
                User user = db.Users.FirstOrDefault(u => u.Username == username);
                if (changePasswordModel.CurrentPassword == user.Password)
                {
                    db.Users.FirstOrDefault(u => u.Username == username).Password = changePasswordModel.NewPassword;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    ViewBag.Message = "Password has been changed";
                }
                else
                {
                    ModelState.AddModelError("Error", "Old Password is not correct.");
                    return RedirectToAction("EditProfile");
                }

            }
            return RedirectToAction("ProfileTemp");
        }
        [HttpPost]
        public ActionResult DeactivateAccount(User userlog)
        {
            var username = Session["Username"];
            User user = db.Users.FirstOrDefault(x => x.Username == username);
            if (userlog.Email == user.Email && userlog.Password == user.Password)
            {
                db.Users.FirstOrDefault(x => x.Username == username).InActive = true;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("login", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult Visit(int id)
        {
            User friend = db.Users.Find(id);
            SearchableModel relation = new SearchableModel();
            var username = Session["Username"];
            User user = db.Users.FirstOrDefault(x => x.Username == username);
            var rel = db.RelationShips.Where(x => (x.Friend.UserID == user.UserID && x.Mate.UserID == id) || (x.Friend.UserID == id && x.Mate.UserID == user.UserID)).Select(
                 x => new
                 {
                     actionuser = x.ActionUser.UserID,
                     state = x.Status,
                 }
                ).SingleOrDefault();
            if (rel != null)
            {
                if (rel.actionuser == id && rel.state == 1)
                    relation.RelationState = 4;
                else
                    relation.RelationState = rel.state;
            }
            else
                relation.RelationState = 0;
            var post = db.Posts.Where(x => x.UserID == friend.UserID).ToList();
            List<CommentsModel> showposts = new List<CommentsModel>();
            foreach (var pos in post)
            {
                var commentss = db.Comment.Where(x => x.PostID == pos.ID).ToList();
                showposts.Add(new CommentsModel { Comments = commentss, post = pos });
            }
            relation.ResultUser = friend;
            relation.FriendPosts = showposts;
            return View(relation);
        }
    }
}