using DonationApplication.data;
using DonationApplication.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonationApplication.web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var userDb = new UserRepository(Properties.Settings.Default.ConStr);
            var user = userDb.GetUserByEmail(User.Identity.Name);
            if (!user.IsAdmin)
            {
                return Redirect("/Home/Index");
            }
            var vm = new DashboardViewModel
            {
                User = user
            };
            return View("Dashboard", vm);
        }

        public ActionResult Categories()
        {
            var catDb = new CategoryRepository(Properties.Settings.Default.ConStr);
            var vm = new CategoriesViewModel
            {
                Categories = catDb.GetCategories()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddCategory(Category cat)
        {
            var catDb = new CategoryRepository(Properties.Settings.Default.ConStr);
            catDb.AddCategory(cat);
            return Redirect("/Admin/Categories");
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category cat)
        {
            var catDb = new CategoryRepository(Properties.Settings.Default.ConStr);
            catDb.UpdateCategory(cat);
            return Redirect("/Admin/Categories");
        }

        public ActionResult Pending()
        {
            return View();
        }

        public ActionResult GetPendingApplications()
        {
            var appDb = new ApplicationRepository(Properties.Settings.Default.ConStr);
            return Json(appDb.GetPendingApplications().Select(a => new
            {
                id = a.Id,
                category = a.Category.Name,
                email = a.User.Email,
                firstName = a.User.FirstName,
                lastName = a.User.LastName,
                donationAmount = a.Amount,
                status = a.Status
            }), JsonRequestBehavior.AllowGet);
        }

        public void UpdateStatus(int applicationId, bool status)
        {
            var appDb = new ApplicationRepository(Properties.Settings.Default.ConStr);
            appDb.UpdateStatus(applicationId, status);

        }

    }
}