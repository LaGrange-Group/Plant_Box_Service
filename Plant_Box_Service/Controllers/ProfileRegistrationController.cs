using Microsoft.AspNet.Identity;
using Plant_Box_Service.Class;
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
    public class ProfileRegistrationController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProfileRegistration
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult CreatePreference(string form = "Survey")
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            registrationViewModel.States = db.States.ToList();
            registrationViewModel.Preference = new Preference();
            db.Preferences.Add(registrationViewModel.Preference);
            db.SaveChanges();
            ViewBag.Sizes = new List<string>() { "Small", "Medium", "Large" };
            return View(registrationViewModel);
        }

        [HttpPost]
        public ActionResult CreatePreference(RegistrationViewModel registrationViewModel)
        {
            db.Entry(registrationViewModel.Preference).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CreateAccount");
        }

        public ActionResult CreateAccount(bool ValidAddress = true)
        {
            ViewBag.IsValid = ValidAddress;
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            int maxPreference = db.Preferences.Max(p => p.Id);
            registrationViewModel.Preference = db.Preferences.Where(p => p.Id == maxPreference).Single();
            registrationViewModel.States = db.States.ToList();
            ViewBag.Sizes = new List<string>() { "Small", "Medium", "Large" };
            return View(registrationViewModel);
        }

        [HttpPost]
        public ActionResult CreateAccount(RegistrationViewModel registrationViewModel)
        {
            var userId = GetId();
            if (Geocode.CheckValidAddress(registrationViewModel.Customer))
            {
                if (Geocode.isValidLocation)
                {
                    registrationViewModel.Customer.UserId = userId;
                    registrationViewModel.Customer.MemberSince = DateTime.Now;
                    registrationViewModel.Customer.Donating = false;
                    registrationViewModel.Customer.AccountStatus = false;
                    registrationViewModel.Customer.PreferenceId = registrationViewModel.Preference.Id;
                    db.Customers.Add(registrationViewModel.Customer);
                    db.SaveChanges();
                    Customer customerForEmail = db.Customers.Include(c => c.State).Where(c => c.UserId == userId).Single();
                    SendEmail.Main(customerForEmail);
                    return RedirectToAction("Main", "Dashboard");
                }
            }
            return RedirectToAction("CreateAccount", new { ValidAddress = false });

        }

        private string GetId()
        {
            string userId = User.Identity.GetUserId();
            return userId;
        }
    }
}