using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Social_Media.Models;

namespace Social_Media.Controllers
{
    public class AccountController : Controller
    {
        MyDbcontext objuser = new MyDbcontext();

        //Registration
        [HttpGet]
        public ActionResult registration(int id = 0)
        {
            User usermodel = new User();
            return View(usermodel);
        }

        [HttpPost]

        public ActionResult registration(User usermodel)
        {
            using (MyDbcontext dbModel = new MyDbcontext())
            {
                if (usermodel.Username == null || usermodel.Email == null || usermodel.Password == null || usermodel.ConfirmPassword == null || usermodel.Birthdate == null || usermodel.ConfirmPassword != usermodel.Password)

                {
                    ViewBag.DuplicateMessage = "cannot registration";
                    return View("registration", usermodel);

                }


                else
                {

                    if (dbModel.Users.Any(x => x.Username == usermodel.Username || x.Email == usermodel.Email))

                    {
                        ViewBag.DuplicateMessage = "Username or Email already exist";
                        return View("registration", usermodel);

                    }

                    else

                        dbModel.Users.Add(usermodel);
                    dbModel.SaveChanges();
                }



            }


            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful.";
            return View("registration", new User());

        }

        //login 

        [HttpGet]
        public ActionResult login()
        {
            User userlog = new User();
            return View(userlog);
        }


        [HttpPost]
        public ActionResult login(Social_Media.Models.User userlog)
        {
            using (MyDbcontext db = new MyDbcontext())
            {
                var userDetails = db.Users.SingleOrDefault(x => x.Email == userlog.Email && x.Password == userlog.Password);
                if (userDetails == null)
                {
                    ModelState.AddModelError("Error", "Wrong Email Or Password");
                    return View();

                }
                else if (userDetails.InActive)
                {
                    ModelState.AddModelError("Error", "Email Deactivated");
                    return View();
                }
                else
                {
                    Session["Username"] = userDetails.Username;
                    return RedirectToAction("Index", "Home");
                }
            }

        }

        //logout 

        public ActionResult logout()

        {
            Session.Abandon();
            return RedirectToAction("login", "Account");

        }

    }
}