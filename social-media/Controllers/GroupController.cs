using Social_Media.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Social_Media.Models;

namespace Social_Media.Controllers
{
    public class GroupController : Controller
    {
        public ActionResult CreateGroup()
        {
            Group g = new Group();
            MyDbcontext db = new MyDbcontext();
            var users = db.Users.ToList();
            ViewBag.data = users;
            return View(users);
        }
        [HttpGet]
        // GET: Group
        [Route("Group/AddMembers/{id}")]
        public ActionResult AddMembers(int id, String group_name, String desc)
        {
            Group g = new Group();
            MyDbcontext db = new MyDbcontext();
            var users = db.Users.ToList();
            ViewBag.data = users;
            g.GroupName = group_name;
            g.Description = desc;
            Console.WriteLine(group_name);
            db.Group.Add(g);
            db.SaveChanges();
            //
            var firstIDS = db.RelationShips.Where(x => x.Status == 3 && x.Friend.UserID == 9).Select(l => l.Mate.UserID).ToList();
            var secIDS = db.RelationShips.Where(x => x.Status == 3 && x.Mate.UserID == 9).Select(l => l.Friend.UserID).ToList();
            List<User> res = new List<User>();
            foreach (var U in secIDS)
            {
                User ID = db.Users.Where(x => x.UserID == U).Single();
                res.Add(ID);
            }
            foreach (var U in firstIDS)
            {
                User ID = db.Users.Where(x => x.UserID == U).Single();
                res.Add(ID);
            }
            var username = Session["Username"];
            User tmp = new User();
            tmp = db.Users.Where(x => x.Username == username.ToString()).Single();
            var userid = tmp.UserID;
            AddMember(id, userid);
            return View(res);
        }

        [HttpGet]
        public void AddMember(int groupid, int MemberID)
        {

            MyDbcontext db = new MyDbcontext();
            Group update = new Group();
            update = db.Group.Where(x => x.GroupID == groupid).Single();
            update.Users.Add(db.Users.Where(x => x.UserID == MemberID).Single());
            db.Group.Attach(update);
            db.Entry(update).Collection(x => x.Users).IsLoaded = true;
            db.SaveChanges();

        }

        [HttpGet]
        public void RemoveMember(int groupid, int MemberID)
        {
            MyDbcontext db = new MyDbcontext();
            Group update = new Group();
            update = db.Group.Where(x => x.GroupID == groupid).Single();
            update.Users.Remove(db.Users.Where(x => x.UserID == MemberID).Single());
            db.Group.Attach(update);
            db.Entry(update).Collection(x => x.Users).IsLoaded = true;
            db.SaveChanges();

        }
        [Route("Group/Group/{id}")]
        public ActionResult Group(int id)
        {
            MyDbcontext db = new MyDbcontext();
            var username = Session["Username"];
            User user = new User();
            user = db.Users.Where(u => u.Username == username.ToString()).FirstOrDefault();
            var firstIDS = db.Users.Where(x => x.UserID == x.UserID).ToList();
            var secIDS = db.Users.Where(x => x.UserID == x.UserID).ToList();
            List<Post> fposts = new List<Post>();

            foreach (var data in secIDS)
                firstIDS.Add(data);
            foreach (var data in firstIDS)
            {
                var friendpost = db.Posts.Where(x => x.UserID == data.UserID).ToList();
                fposts.AddRange(friendpost);
            }
            var post = db.Posts.Where(x => x.GroupID == id).ToList();
            List<CommentsModel> showposts = new List<CommentsModel>();
            foreach (var pos in post)
            {
                var commentss = db.Comment.Where(x => x.PostID == pos.ID).ToList();
                showposts.Add(new CommentsModel { Comments = commentss, post = pos });
            }
            ProfileModel profile = new ProfileModel { Friends = firstIDS, LoggedUser = user, Posts = showposts, FriendPosts = showposts };
            return View(profile);
        }
        [Route("Group/Members/{id}")]
        public ActionResult Members(int id)
        {
            MyDbcontext db = new MyDbcontext();
            var members = db.Group.Where(x => x.GroupID == id).Select(u => u.Users).SingleOrDefault().ToList();
            return View(members);
        }
        [Route("Group/DeleteGroup/{id}")]
        public ActionResult DeleteGroup(int id)
        {
            MyDbcontext db = new MyDbcontext();
            Group update = new Group();
            update = db.Group.Where(x => x.GroupID == id).Single();
            db.Group.Remove(update);
            db.SaveChanges();
            return RedirectToAction("index", "Home");
        }
        public ActionResult LeaveGroup(int groupid, int MemberID)
        {
            MyDbcontext db = new MyDbcontext();
            Group update = new Group();
            update = db.Group.Where(x => x.GroupID == groupid).Single();
            update.Users.Remove(db.Users.Where(x => x.UserID == MemberID).Single());
            db.Group.Attach(update);
            db.Entry(update).Collection(x => x.Users).IsLoaded = true;
            db.SaveChanges();
            return RedirectToAction("index", "Home");
        }

        public ActionResult Create(Post post, int id)
        {
            MyDbcontext db = new MyDbcontext();
            var username = Session["Username"];
            User user1 = new User();
            user1 = db.Users.Where(u => u.Username == username).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                var posts = new Post();

                return View("Create", post);
            }

            if (post.ID == 0)

                post.like_post = 0;
            post.DateTime = DateTime.Now;
            post.UserID = (byte)user1.UserID;
            post.User = db.Users.FirstOrDefault(user => user.UserID == post.UserID);
            post.DateTime = DateTime.Now;
            post.GroupID = id;
            db.Posts.Add(post);
            db.SaveChanges();



            return RedirectToAction("Index", "Home");


        }

    }
}