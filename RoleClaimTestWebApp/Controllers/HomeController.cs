using System;
using System.Security.Claims;
using System.Web.Mvc;

namespace RoleClaimTestWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = User.IsInRole("approver") ? "Hey! welcome approver." : "Hey! welcome non approver.";
            return View();
        }

        [Authorize(Roles = "approver")]
        public ActionResult Contact()
        {
            string userfirstname = ClaimsPrincipal.Current.FindFirst(ClaimTypes.GivenName).Value;
            ViewBag.Message = String.Format("Welcome, {0}!", userfirstname);

            return View();
        }
    }
}