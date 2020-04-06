using SeaTrack.Lib.DTO;
using SeaTrack.Lib.DTO.Admin;
using SeaTrack.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaTrack.Areas.Admin.Controllers
{
    public class AgencyController : Controller
    {
        // GET: Admin/Agency
        public ActionResult Customer()
        {
            if (CheckRole(2) == -1)
            {
                if (CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            return View();
        }

        public new ActionResult User()
        {
            if (CheckRole(2) == -1)
            {
                if (CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            return View();
        }

        public ActionResult Device()
        {
            if (CheckRole(2) == -1)
            {
                if (CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            return View();
        }

        //public ActionResult DeviceDetail(int id)
        //{

        //    DeviceViewModel device = AdminService.GetDeviceByID(id);
        //    return View(device);
        //}
        public ActionResult Detail(int id)
        {
            if (CheckRole(2) == -1)
            {
                if (CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            var user = (Users)Session["User"];
            if (TempData["EditResult"] != null)
            {
                ViewBag.EditResult = TempData["EditResult"].ToString();
            }
            UserViewModel us = new UserViewModel();
            us = AdminService.GetUserByID(id);
            if (us.ManageBy == user.Username || AdminService.CheckUserManage(id, user.Username))
            {
                return View(us);
            }
            if (us.RoleID == 3)
            {
                return RedirectToAction("Customer", "Agency");
            }
            return RedirectToAction("User", "Agency");

        }
        //[HttpPost]
        //public ActionResult Update(DeviceViewModel device)
        //{
        //    Device dv = new Lib.DTO.Device();
        //    dv.DeviceID = device.DeviceID;
        //    dv.DeviceNo = device.DeviceNo;
        //    dv.DeviceName = device.DeviceName;
        //    dv.DeviceVersion = device.DeviceVersion;
        //    dv.DeviceImei = device.DeviceImei;
        //    dv.DeviceGroup = device.DeviceGroup;
        //    dv.DeviceNote = device.DeviceNote;
        //    dv.DateExpired = device.ExpireDate;
        //    var res = AdminService.UpdateDevice(dv);
        //    return RedirectToAction("DeviceDetail", new { id = dv.DeviceID });
        //}
        [HttpGet]
        public ActionResult GetListUserByUserID(int id) //id = RoleID
        {
            if (CheckRole(2) == -1)
            {
                if (CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            //string Username = Request.Cookies["Username"].Value.ToString();
            var user = (Users)Session["User"];
            var rs = AdminService.GetListUserByUserID(user.Username, id);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetListUserOfAgency() //id = RoleID
        {
            if (CheckRole(2) == -1)
            {
                if (CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            //string Username = Request.Cookies["Username"].Value.ToString();
            var user = (Users)Session["User"];
            var rs = AdminService.GetListUserOfAgency(user.Username);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        public int CheckRole(int role)
        {
            var user = (Users)Session["User"];
            if (user != null)
            {
                if (user.RoleID == role)
                {
                    return -1; //sai quyền
                }
                return 0; //Chưa đăng nhập
            }
            return 1; //Hợp lệ
        }

    }
}