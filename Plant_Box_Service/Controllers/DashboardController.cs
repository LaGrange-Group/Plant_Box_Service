using Microsoft.AspNet.Identity;
using Plant_Box_Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trash_Collector.Class;

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
                dashboardViewModel.Customer = db.Customers.Include(c => c.State).Where(c => c.UserId == userId).Single();
                try
                {
                    dashboardViewModel.Gift = db.Gifts.Where(g => g.Id == dashboardViewModel.Customer.GiftId).Single();
                }
                catch
                {

                }
                dashboardViewModel.Preference = db.Preferences.Where(p => p.Id == dashboardViewModel.Customer.PreferenceId).Single();
                dashboardViewModel.Payments = db.Payments.Where(p => p.CustomerId == dashboardViewModel.Customer.Id).ToList();
                dashboardViewModel.Shipments = db.Shipments.Where(s => s.CustomerId == dashboardViewModel.Customer.Id).ToList();
                try
                {
                    int maxPaymentId = dashboardViewModel.Payments.Max(p => p.Id);
                    dashboardViewModel.Payment = db.Payments.Where(p => p.CustomerId == dashboardViewModel.Customer.Id && p.Id == maxPaymentId).Single();
                }
                catch
                {

                }
                ViewBag.Sizes = new List<string>() { "Small", "Medium", "Large" };
                return View(dashboardViewModel);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult Main (DashboardViewModel model)
        {
            if (model.UpdatePreferences == true)
            {
                db.Entry(model.Preference).State = EntityState.Modified;
                db.SaveChanges();
                System.Threading.Thread.Sleep(2000);
                return RedirectToAction("Main");
            }
            return RedirectToAction("Index");
        }

        public ActionResult ActivateAccount()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ActivateAccount(int Id)
        {
            return View();
        }

        public ActionResult ConfrimUpdate()
        {
            return PartialView();
            
        }
        [HttpPost]
        public ActionResult ConfrimUpdate(int Id)
        {
            return RedirectToAction("Main");
        }

        public ActionResult GiftForm()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.Gift = new Gift();
            dashboardViewModel.Gift.isContinuous = false;
            db.Gifts.Add(dashboardViewModel.Gift);
            db.SaveChanges();
            dashboardViewModel.States = db.States.ToList();
            return PartialView(dashboardViewModel);
        }
        [HttpPost]
        public ActionResult GiftForm(DashboardViewModel dashboardViewModel)
        {
            db.Entry(dashboardViewModel.Gift).State = EntityState.Modified;
            var userId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(c => c.UserId == userId).Single();
            customer.GiftId = dashboardViewModel.Gift.Id;
            customer.Gifting = true;
            if (customer.Donating == true)
            {
                customer.Donating = false;
            }
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Main");
        }

        public ActionResult GiftUpdate()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            var userId = GetId();
            int? customerId = db.Customers.Where(c => c.UserId == userId).Select(c => c.GiftId).Single();
            dashboardViewModel.Gift = db.Gifts.Where(g => g.Id == customerId).Single();
            dashboardViewModel.States = db.States.ToList();
            return PartialView(dashboardViewModel);
        }
        [HttpPost]
        public ActionResult GiftUpdate(DashboardViewModel dashboardViewModel)
        {
            db.Entry(dashboardViewModel.Gift).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Main");
        }

        public ActionResult GiftStatus()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            var userId = GetId();
            dashboardViewModel.Customer = db.Customers.Where(c => c.UserId == userId).Single();
            return PartialView(dashboardViewModel);
        }
        //[HttpPost]
        //public ActionResult GiftStatus(DashboardViewModel dashboardViewModel)
        //{
        //    return RedirectToAction("Main");
        //}

        public ActionResult TurnOffGifting(int customerId)
        {
            Customer customer = db.Customers.Where(c => c.Id == customerId).Single();
            customer.Gifting = false;
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Main");
        }

        public ActionResult DonatingStatus()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            var userId = GetId();
            dashboardViewModel.Customer = db.Customers.Where(c => c.UserId == userId).Single();
            return PartialView(dashboardViewModel);
        }
        [HttpPost]
        public ActionResult DonatingStatus(int customerId)
        {
            return RedirectToAction("Main");
        }

        public ActionResult ToggleDonating(int customerId)
        {
            Customer customer = db.Customers.Where(c => c.Id == customerId).Single();
            customer.Donating = !customer.Donating;
            if (customer.Donating == null)
            {
                customer.Donating = true;
            }
            if (customer.Donating == true)
            {
                customer.Gifting = false;
            }
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Main");
        }

        public ActionResult PersonalUpdate(bool isValid = true)
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            var userId = GetId();
            dashboardViewModel.Customer = db.Customers.Where(c => c.UserId == userId).Single();
            dashboardViewModel.States = db.States.ToList();
            return PartialView(dashboardViewModel);
        }
        [HttpPost]
        public ActionResult PersonalUpdate(DashboardViewModel dashboardViewModel)
        {
            if (Geocode.CheckValidAddress(dashboardViewModel.Customer))
            {
                if (Geocode.isValidLocation)
                {
                    db.Entry(dashboardViewModel.Customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Main", "Dashboard");
                }
            }
            return RedirectToAction("PersonalUpdate", new { isValid = false});
        }

        public ActionResult PersonalStatus()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            var userId = GetId();
            dashboardViewModel.Customer = db.Customers.Where(c => c.UserId == userId).Single();
            return PartialView(dashboardViewModel);
        }
        [HttpPost]
        public ActionResult PersonalStatus(DashboardViewModel dashboardViewModel)
        {
            return RedirectToAction("Main");
        }

        public ActionResult DeactivateAccount(int customerId)
        {
            Customer customer = db.Customers.Where(c => c.Id == customerId).Single();
            customer.AccountStatus = false;
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Main");
        }
        private string GetId()
        {
            string userId = User.Identity.GetUserId();
            return userId;
        }
    }
}