using DonationApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DonationApplication.web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string email, string password)
        {
            var db = new UserRepository(Properties.Settings.Default.ConStr);
            db.AddUser(firstName, lastName, email, password, false);
            return Redirect("/home/index/");
        }

        public ActionResult CheckEmailExists(string email)
        {
            var db = new UserRepository(Properties.Settings.Default.ConStr);
            return Json(new { exists = db.EmailExists(email) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var db = new UserRepository(Properties.Settings.Default.ConStr);
            User user = db.Login(email, password);
            if (user == null)
            {
                return Redirect("/account/login");
            }
            FormsAuthentication.SetAuthCookie(email, true);
            return Redirect("/home/index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/home/index");
        }
    }
}