using DonationApplication.data;
using DonationApplication.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonationApplication.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var userDb = new UserRepository(Properties.Settings.Default.ConStr);
                var user = userDb.GetUserByEmail(User.Identity.Name);
                if (user.IsAdmin)
                {
                    return Redirect("/Admin/Index");
                }
                var vm = new DashboardViewModel
                {
                    User = user
                };
                return View("Dashboard", vm);
            }
      
            return View();
        }

        [Authorize]
        public ActionResult Application()
        {
            var catDb = new CategoryRepository(Properties.Settings.Default.ConStr);
            var vm = new ApplicationViewModel
            {
                Categories = catDb.GetCategories()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Application(int catId, decimal amount, string description)
        {
            var userDb = new UserRepository(Properties.Settings.Default.ConStr);
            var user = userDb.GetUserByEmail(User.Identity.Name);
            var appDb = new ApplicationRepository(Properties.Settings.Default.ConStr);
            appDb.AddApplication(new Application
            {
                UserId = user.Id,
                DateCreated = DateTime.Now,
                Amount = amount,
                Description = description,
                CategoryId = catId,
                Status = Status.Pending
            });
            return Redirect("/home/history");
        }
        
        [Authorize]
        public ActionResult History()
        {
            var appDb = new ApplicationRepository(Properties.Settings.Default.ConStr);
            var userDb = new UserRepository(Properties.Settings.Default.ConStr);
            var user = userDb.GetUserByEmail(User.Identity.Name);
            var vm = new HistoryViewModel
            {
                Applications = appDb.GetApplicationHistory(user.Id),
                User = user
            };
            return View(vm);
        }
    }
}