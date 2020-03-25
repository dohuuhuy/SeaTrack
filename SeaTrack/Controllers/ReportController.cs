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
    public class ReportController : Controller
    {
        public ActionResult Ministry()
        {
            if (Request.Cookies["userName"] == null && Request.Cookies["pass"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View("Ministry");
        }
        public ActionResult Business()
        {
            if (Request.Cookies["userName"] == null && Request.Cookies["pass"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View("Business");
        }
    }
}
