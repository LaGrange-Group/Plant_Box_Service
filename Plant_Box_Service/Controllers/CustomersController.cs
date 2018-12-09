using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Plant_Box_Service.Models;
using Trash_Collector.Class;

namespace Plant_Box_Service.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            //var customers = db.Customers.Include(c => c.Payment).Include(c => c.Preference).Include(c => c.State);
            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create(int? preferenceId, bool isValid = true, string firstName = "", string lastName = "")
        {
            Customer customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                PreferenceId = preferenceId
            };
            ViewBag.isValid = isValid;
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Id");
            ViewBag.PreferenceId = new SelectList(db.Preferences, "Id", "OptimalSize");
            ViewBag.StateId = new SelectList(db.States, "Id", "Name");
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,City,StateId,ZipCode,PreferenceId,PaymentId,Gifting,Donating,AccountStatus,MemberSince,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                customer.UserId = userId;
                customer.MemberSince = DateTime.Now;
                customer.Donating = false;
                if (Geocode.CheckValidAddress(customer))
                {
                    if (Geocode.isValidLocation)
                    {
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                return RedirectToAction("Create", new { customer.PreferenceId, isValid = false, firstName = customer.FirstName, lastName = customer.LastName });
            }

            //ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Id", customer.PaymentId);
            ViewBag.PreferenceId = new SelectList(db.Preferences, "Id", "OptimalSize", customer.PreferenceId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", customer.StateId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            //ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Id", customer.PaymentId);
            ViewBag.PreferenceId = new SelectList(db.Preferences, "Id", "OptimalSize", customer.PreferenceId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", customer.StateId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,City,StateId,ZipCode,PreferenceId,PaymentId,Gifting,Donating,AccountStatus,MemberSince,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Id", customer.PaymentId);
            ViewBag.PreferenceId = new SelectList(db.Preferences, "Id", "OptimalSize", customer.PreferenceId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", customer.StateId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
