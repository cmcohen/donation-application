using DonationApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonationApplication.web
{
    public class LayoutDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userDb = new UserRepository(Properties.Settings.Default.ConStr);
                filterContext.Controller.ViewBag.User = userDb.GetUserByEmail(filterContext.HttpContext.User.Identity.Name);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}