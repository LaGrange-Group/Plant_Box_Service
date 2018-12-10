using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plant_Box_Service.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Dashboard");
            }else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("ToDoShipments", "Admin");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}