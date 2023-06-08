using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Social_Media.Models;
using Social_Media.ViewModel;
namespace Social_Media.Controllers
{
    public class SearchController : Controller
    {
        const int Pending = 1;
        const int Blocked = 2;
        const int Accepted = 3;
        private MyDbcontext db = new MyDbcontext();
        [HttpGet]
        public ActionResult all(string search)
        {

            String namee = (String)Session["Username"];
            User login = db.Users.Where(x => x.Username.Equals(namee)).FirstOrDefault();
            ViewBag.Login = login.UserID;
            List<SearchableModel> serachList = new List<SearchableModel>();
            var result = db.Users.Where(x => x.Username.StartsWith(search)).ToList();

            foreach (var user in result)
            {
                int alreadFreiends = db.RelationShips.Where(x => ((x.Mate.UserID.Equals(login.UserID) && x.Friend.UserID == user.UserID) || (x.Friend.UserID.Equals(login.UserID)) && x.Mate.UserID == user.UserID) && x.Status == Accepted).Select(l => l.Status).SingleOrDefault();
                int CancelRequest = db.RelationShips.Where(x => x.ActionUser.UserID.Equals(login.UserID) && (x.Mate.UserID == user.UserID || x.Friend.UserID == user.UserID) && x.Status == Pending).Select(l => l.Status).FirstOrDefault();
                int PendingRequest = db.RelationShips.Where(x => x.ActionUser.UserID.Equals(user.UserID) && (x.Mate.UserID == login.UserID || x.Friend.UserID == login.UserID) && x.Status == Pending).Select(l => l.Status).FirstOrDefault();
                int block = db.RelationShips.Where(x => ((x.Mate.UserID.Equals(login.UserID) && x.Friend.UserID == user.UserID) || (x.Friend.UserID.Equals(login.UserID)) && x.Mate.UserID == user.UserID) && x.Status == Blocked).Select(l => l.Status).SingleOrDefault();

                if (PendingRequest == 1)
                    PendingRequest = 4;
                if (block == 0)
                    serachList.Add(new SearchableModel { ResultUser = user, RelationState = Math.Max(Math.Max(alreadFreiends, CancelRequest), PendingRequest) });
            }

            return View(serachList);
        }
        [HttpGet]
        public void AddFriend(int friendID)
        {
            String namee = (String)Session["Username"];
            User login = db.Users.Where(x => x.Username.Equals(namee)).FirstOrDefault();
            var mate = db.Users.Where(x => x.UserID.Equals(login.UserID)).Single();
            var friend = db.Users.Where(x => x.UserID.Equals(friendID)).Single();
            db.RelationShips.Add(new RelationShip { Mate = mate, Friend = friend, ActionUser = mate, Status = Pending });
            db.SaveChanges();

        }
        [HttpGet]
        public void CancelRequest(int friendID)
        {
            String namee = (String)Session["Username"];
            User login = db.Users.Where(x => x.Username.Equals(namee)).FirstOrDefault();
            int Relation = db.RelationShips.Where(x => ((x.Mate.UserID == friendID) && (x.Friend.UserID == login.UserID)) || ((x.Friend.UserID == friendID) && (x.Mate.UserID == login.UserID)))
                       .Select(l => l.RelationID).SingleOrDefault();
            db.RelationShips.Remove(db.RelationShips.Where(x => x.RelationID == Relation).Single());
            db.SaveChanges();
        }
        [HttpGet]
        public void AcceptFriendRequest(int friendID)
        {
            String namee = (String)Session["Username"];
            User login = db.Users.Where(x => x.Username.Equals(namee)).FirstOrDefault();
            int Relation = db.RelationShips.Where(x => ((x.Mate.UserID == friendID) && (x.Friend.UserID == login.UserID)) || ((x.Friend.UserID == friendID) && (x.Mate.UserID == login.UserID)))
                       .Select(l => l.RelationID).SingleOrDefault();
            RelationShip update = new RelationShip();
            update = db.RelationShips.Where(x => x.RelationID == Relation).Single();
            update.Status = 3;
            db.RelationShips.Attach(update);
            db.Entry(update).Property(x => x.Status).IsModified = true;
            db.SaveChanges();
        }
        [HttpGet]
        public JsonResult friendrequests()
        {
            String namee = (String)Session["Username"];
            User login = db.Users.Where(x => x.Username.Equals(namee)).FirstOrDefault();
            var FirstPendingRequest = db.RelationShips.Where(x => x.ActionUser.UserID != login.UserID && x.Mate.UserID == login.UserID && x.Status == 1).Select(
                x => new
                {
                    name = x.Friend.Username,
                    id = x.Friend.UserID,
                    img = x.Friend.Pic,
                }).ToList();
            var SecPendingRequest = db.RelationShips.Where(x => x.ActionUser.UserID != login.UserID && x.Friend.UserID == login.UserID && x.Status == 1).Select(
                x => new
                {
                    name = x.Mate.Username,
                    id = x.Mate.UserID,
                    img = x.Mate.Pic,
                }
                ).ToList();
            foreach (var user in SecPendingRequest)
                FirstPendingRequest.Add(user);

            return Json(FirstPendingRequest, JsonRequestBehavior.AllowGet);
        }

    }
}