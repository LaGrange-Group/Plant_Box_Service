using Microsoft.AspNet.Identity;
using Plant_Box_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plant_Box_Service.Controllers
{
    public class DashboardController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            var userId = GetId();
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.Customer = db.Customers.Where(c => c.UserId == userId).Single();
            return View();
        }
        private string GetId()
        {
            string userId = User.Identity.GetUserId();
            return userId;
        }
    }
}