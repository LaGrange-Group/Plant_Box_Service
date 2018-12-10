using Plant_Box_Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plant_Box_Service.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.Customers = db.Customers.Where(c => c.AccountStatus == true).ToList();
            adminViewModel.Payments = db.Payments.ToList();
            return View(adminViewModel);
        }

        public ActionResult ToDoShipments()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.Customers = db.Customers.Where(c => c.AccountStatus == true).ToList();
            adminViewModel.Payments = db.Payments.ToList();
            adminViewModel.Gifts = db.Gifts.ToList();
            adminViewModel.Shipments = db.Shipments.Where(s => s.Status == false && s.Delivered == false).ToList();
            return View(adminViewModel);
        }

        public ActionResult ViewForm(int customerId = 0)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.Customers = db.Customers.Where(c => c.AccountStatus == true).ToList();
            adminViewModel.Payments = db.Payments.ToList();
            adminViewModel.Gifts = db.Gifts.ToList();
            adminViewModel.Shipments = db.Shipments.Where(s => s.Status == false && s.Delivered == false).ToList();
            adminViewModel.Customer = db.Customers.Include("State").Include("Gift").Where(c => c.Id == customerId).Single();
            adminViewModel.Preference = db.Preferences.Where(p => p.Id == adminViewModel.Customer.PreferenceId).Single();
            adminViewModel.Shipment = db.Shipments.Where(s => s.CustomerId == adminViewModel.Customer.Id).OrderByDescending(s => s.Id).FirstOrDefault();
            return View(adminViewModel);
        }

        public ActionResult UpdateTrackingHash()
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.Customers = db.Customers.Where(c => c.AccountStatus == true).ToList();
            adminViewModel.Payments = db.Payments.ToList();
            adminViewModel.Gifts = db.Gifts.ToList();
            adminViewModel.Shipments = db.Shipments.Where(s => s.Status == false && s.Delivered == false).ToList();
            return View(adminViewModel);
        }

        public ActionResult TrackingHashForm(int customerId = 0)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.Customers = db.Customers.Where(c => c.AccountStatus == true).ToList();
            adminViewModel.Payments = db.Payments.ToList();
            adminViewModel.Gifts = db.Gifts.ToList();
            adminViewModel.Shipments = db.Shipments.Where(s => s.Status == false && s.Delivered == false).ToList();
            adminViewModel.Customer = db.Customers.Include("State").Include("Gift").Where(c => c.Id == customerId).Single();
            adminViewModel.Preference = db.Preferences.Where(p => p.Id == adminViewModel.Customer.PreferenceId).Single();
            adminViewModel.Shipment = db.Shipments.Where(s => s.CustomerId == adminViewModel.Customer.Id).OrderByDescending(s => s.Id).FirstOrDefault();
            return View(adminViewModel);
        }
        [HttpPost]
        public ActionResult TrackingHashForm(AdminViewModel adminViewModel)
        {
            Customer customerToUpdate = db.Customers.Include(c => c.Gift).Where(c => c.Id == adminViewModel.Customer.Id).Single();
            if (customerToUpdate.Gift.isContinuous == false)
            {
                customerToUpdate.Gifting = false;
                db.Entry(customerToUpdate).State = EntityState.Modified;
                db.SaveChanges();
            }
            adminViewModel.Shipment.Status = true;
            adminViewModel.Shipment.DateShipped = DateTime.Now;
            db.Entry(adminViewModel.Shipment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UpdateTrackingHash");
        }

    }
}