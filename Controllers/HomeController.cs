using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Tourisme_MVC_projet.Models;


namespace Tourisme_MVC_projet.Controllers
{
    public class HomeController : Controller
    {

        mydbEntities3 db = new mydbEntities3();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); //
                db.Users.Add(user);


                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public bool IsEmailExist(string emailID)
        {
            using (mydbEntities3 dc = new mydbEntities3())
            {
                var v = dc.Users.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users login)
        {
            if (ModelState.IsValid)
            {
                mydbEntities3 db = new mydbEntities3();
                var v = db.Users.Where(a => a.Email.Equals(login.Email) && a.Password.Equals(login.Password)).FirstOrDefault();
                if (v != null)
                {
                    Session["LogedUserID"] = v.UserId.ToString();
                    Session["LogedUserFullname"] = v.Name.ToString();
                    return RedirectToAction("AfterLogin");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View(login);
        }
        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}