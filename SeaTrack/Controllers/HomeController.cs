using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetNuke.Common.Utilities;
using SeaTrack.Lib.DTO;
using SeaTrack.Lib.Service;
using SeaTrack.Models;

namespace SeaTrack.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomeTracking()
        {
            if (Request.Cookies["userName"] == null && Request.Cookies["pass"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Route()
        {
            if (Request.Cookies["userName"] == null && Request.Cookies["pass"] == null)
            {
                return RedirectToAction("Login");
            }
            return View("Route");
        }
        public ActionResult Login()
        {
            if (Request.Cookies["userName"] != null && Request.Cookies["pass"] != null)
            {
                return RedirectToAction("HomeTracking", "Home");
            }
            Session["userName"] = Request.Cookies["userName"] == null ? "" : Request.Cookies["userName"].Value;
            Session["pass"] = Request.Cookies["pass"] == null ? "" : Request.Cookies["pass"].Value;
            return View("Login");
        }
        public ActionResult ValidateUser(FormCollection from)
        {
            if (Request.Cookies["userName"] != null && Request.Cookies["pass"] != null)
            {
                return RedirectToAction("HomeTracking", "Home");
            }
            String username_ = from["username"];
            String password_ = from["password"];
            String ckRemember = from["ckRemember"];
            ckRemember = null;
            if (!String.IsNullOrEmpty(username_) || !String.IsNullOrEmpty(password_))
            {
                Users useritem = UsersService.CheckUsers(username_, password_);
                
                if (useritem != null && useritem.RoleID != 1 && useritem.RoleID != 2)
                {
                    FormsAuthentication.SetAuthCookie(useritem.Username, true);
                    if (!String.IsNullOrEmpty(ckRemember))
                    {
                        addCookie(useritem, true);
                    }
                    else
                    {
                        addCookie(useritem, false);
                    }
                    return RedirectToAction("Route", "Home");
                }
                else
                {
                    Session["statusLogin"] = "0";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                Session["statusLogin"] = "0";
                return RedirectToAction("Login");
            }
        }
        public void addCookie(Users firstOrDefault, bool remember)
        {
            Response.Cookies.Add(new HttpCookie("userName")
            {
                Value = firstOrDefault.Username.ToString(),
                Expires = DateTime.Now.AddMinutes(90)

            });
            Response.Cookies.Add(new HttpCookie("pass")
            {
                Value = firstOrDefault.Password.ToString(),
                Expires = DateTime.Now.AddMinutes(90)

            });
            if (remember)
            {
                Response.Cookies.Add(new HttpCookie("userName_log")
                {
                    Value = firstOrDefault.Username.ToString(),
                    Expires = DateTime.Now.AddDays(30)

                });
                Response.Cookies.Add(new HttpCookie("pass_log")
                {
                    Value = firstOrDefault.Password.ToString(),
                    Expires = DateTime.Now.AddDays(30)

                });
            }
            else
            {
                if (Request.Cookies["userName_log"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("userName_log");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                }
                if (Request.Cookies["pass_log"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("pass_log");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                }
            }
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["userName"] = null;
            Session["pass"] = null;

            if (Request.Cookies["pass"] != null)
            {
                HttpCookie myCookie = new HttpCookie("pass");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            if (Request.Cookies["userName"] != null)
            {
                HttpCookie myCookie = new HttpCookie("userName");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult GetListDeviceStatus()
        {
            var data = TrackDataService.GetListDeviceStatus();
            return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetLastedLocation(int deviceID)
        {
            var data = TrackDataService.GetLastedLocation(deviceID);
            return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetRoadmap(int deviceID)
        {
            var data = TrackDataService.GetRoadmap(deviceID);
            return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetRoadmapByDateTime(int deviceID, string From, string To)
        {
            DateTime fromtime = Convert.ToDateTime(DateTime.ParseExact(From, "dd-M-yyyy HH:mm", CultureInfo.InvariantCulture));
            DateTime totime = Convert.ToDateTime(DateTime.ParseExact(To, "dd-M-yyyy HH:mm", CultureInfo.InvariantCulture));

            //DateTime fromtime = Convert.ToDateTime(From);
            //DateTime totime = Convert.ToDateTime(To);

            var data = TrackDataService.GetRoadmapByDateTime(deviceID, fromtime, totime);
            return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetListDevice()
        {
            var data = TrackDataService.GetListDevice();
            return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult UserInfo()
        {
            Users uitem = null;
            if (Request.Cookies["userName"] != null && Request.Cookies["pass"] != null)
            {
                var name = Request.Cookies["userName"].Value;
                var pass = Request.Cookies["pass"].Value;
                uitem = UsersService.CheckUsers(name,pass);
            }
            if (uitem != null)
            {
                return Json(uitem, JsonRequestBehavior.AllowGet);
            }
            return Json("0001", JsonRequestBehavior.AllowGet);
        }
    }
}
