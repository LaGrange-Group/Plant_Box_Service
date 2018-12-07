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
            return RedirectToAction("Main");
        }

        public ActionResult Main()
        {
            try
            {
                var userId = GetId();
                DashboardViewModel dashboardViewModel = new DashboardViewModel();
                dashboardViewModel.Customer = db.Customers.Where(c => c.UserId == userId).Single();
                dashboardViewModel.Preference = db.Preferences.Where(p => p.Id == dashboardViewModel.Customer.PreferenceId).Single();
                dashboardViewModel.Payments = db.Payments.Where(p => p.CustomerId == dashboardViewModel.Customer.Id).ToList();
                try
                {
                    int maxPaymentId = dashboardViewModel.Payments.Max(p => p.Id);
                    dashboardViewModel.Payment = db.Payments.Where(p => p.CustomerId == dashboardViewModel.Customer.Id && p.Id == maxPaymentId).Single();
                }
                catch
                {

                }

                return View(dashboardViewModel);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }

        }
        private string GetId()
        {
            string userId = User.Identity.GetUserId();
            return userId;
        }
    }
}