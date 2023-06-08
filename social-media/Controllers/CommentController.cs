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
    public class CommentController : Controller
    {
        private MyDbcontext _context;
        public CommentController()
        {
            _context = new MyDbcontext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Comment
        public ActionResult Index()
        {
            var Comments = _context.Comment.ToList();
            return View(Comments);
        }
        public ActionResult AddComment(int id ,String str)
        {
            var username = Session["Username"];
            User user1 = new User();
            Comment comment = new Comment();
            comment.Content = str.Substring(8);
            user1 = _context.Users.Where(u => u.Username == username.ToString()).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                var comments = new Comment();
                
                return View("AddComment", comments);
            }

            if (comment.CommentID == 0)

            comment.UserID = (byte)user1.UserID;
            comment.User = _context.Users.FirstOrDefault(user => user.UserID == comment.UserID);
            comment.PostID = (byte)id;
            _context.Comment.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Index", "comment");



        }
        public ActionResult Delete(int? id)
        {
            var CommentInDb = _context.Comment.Single(p => p.CommentID == id);
            if (CommentInDb == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            _context.Comment.Remove(CommentInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Comment");

        }
    }
}

    
