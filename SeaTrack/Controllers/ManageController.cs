using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace SeaTrack.Controllers
{
    public class ManageController : Controller
    {
        public ActionResult Customer()
        {
            if (Request.Cookies["userName"] == null && Request.Cookies["pass"] == null)
            {
                return RedirectToAction("Login","Home");
            }
            return View("Customer");
        }
        public ActionResult Device()
        {
            if (Request.Cookies["userName"] == null && Request.Cookies["pass"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View("Device");
        }
        public ActionResult Driver()
        {
            if (Request.Cookies["userName"] == null && Request.Cookies["pass"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View("Driver");
        }
    }
}
