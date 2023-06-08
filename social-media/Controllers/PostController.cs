using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Social_Media.Models;
using System.Net;

namespace Social_Media.Controllers
{

    public class PostController : Controller
    {

        private MyDbcontext _context;
        public PostController()
        {
            _context = new MyDbcontext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Post
        public ActionResult Index()
        {
            var post = _context.Posts.Include(p => p.User).ToList();
            return View(post);
        }
        public ActionResult FriendsPosts()
        {
            var post = _context.Posts.Include(p => p.User).ToList();
            return View(post);
        }

        public ActionResult Create(Post post)
        {
            var username = Session["Username"];
            User user1 = new User();
            user1 = _context.Users.Where(u => u.Username == username).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                var posts = new Post();

                return View("Create", post);
            }

            if (post.ID == 0)

                post.like_post = 0;
            post.DateTime = DateTime.Now;
            post.UserID = (byte)user1.UserID;
            post.User = _context.Users.FirstOrDefault(user => user.UserID == post.UserID);
            post.DateTime = DateTime.Now;
            _context.Posts.Add(post);
            _context.SaveChanges();



            return RedirectToAction("Index", "Home");


        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var PostInDb = _context.Posts.Single(p => p.ID == id);
            if (PostInDb == null)
                return HttpNotFound();


            return View(PostInDb);

        }



        [HttpPost]
        public ActionResult Edit(Post post)
        {
            var PostInDb = _context.Posts.Single(p => p.ID == post.ID);

            if (ModelState.IsValid)
            {

                PostInDb.Body = post.Body;
                post.DateTime = DateTime.Now;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        public ActionResult AddEditPost(int? postId)
        {
            if (postId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var PostInDb = _context.Posts.Single(p => p.ID == postId);
            if (PostInDb == null)
                return HttpNotFound();


            return View("Partial2", PostInDb);
        }

        [HttpPost]
        public ActionResult SaveData(Post post)
        {

            var username = Session["Username"];
            User user1 = new User();
            user1 = _context.Users.Where(u => u.Username == username).FirstOrDefault();

            try
            {
                MyDbcontext db = new MyDbcontext();


                if (post.ID > 0)
                {
                    //update
                    Post emp = db.Posts.SingleOrDefault(x => x.ID == post.ID);

                    emp.Body = post.Body;
                    emp.DateTime = DateTime.Now;
                    db.SaveChanges();


                }
                else
                {
                    //Insert
                    Post emp = new Post();
                    emp.UserID = (byte)user1.UserID;
                    emp.User = _context.Users.FirstOrDefault(user => user.UserID == emp.UserID);
                    emp.DateTime = DateTime.Now;
                    _context.Posts.Add(emp);
                    _context.SaveChanges();

                }
                return View(post);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public ActionResult Delete(int? id)
        {
            var PostInDb = _context.Posts.Single(p => p.ID == id);
            if (PostInDb == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            _context.Posts.Remove(PostInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult like(int id)
        {
            Post update = _context.Posts.ToList().Find(m => m.ID == id);
            update.like_post += 1;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }


}