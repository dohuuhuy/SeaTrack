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
    public class DeviceController : Controller
    {
        // GET: Admin/Device
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Show()
        {
            return View();
        }
     
        [HttpGet]
        public ActionResult GetListDevice()  
        {
            var data = AdminService.GetListDevice();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDeviceByID(int id)
        {
            var data = AdminService.GetDeviceByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetListDeviceByUserID()
        {
            var user = (Users)Session["User"];
            var data = AdminService.GetListDeviceByUserID(user.UserID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetListDeviceStatus()
        {
            var data = TrackDataService.GetListDeviceStatus();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateDevice(Device device)
        {
            //try
            //{
                var rs = AdminService.CreateDevice(device);
                return Json(new { success = true });
            //}
            //catch (Exception)
            //{
            //    return Json(new { success = false });
            //}
        }
        [HttpPost]
        public JsonResult AgencyCreateDevice(Device device)
        {
            
            var user = (Users)Session["User"];
            device.CreateBy = user.Username;
            device.DateCreate = DateTime.Now;
            device.StatusDevice = 1;
            var rs = AdminService.CreateDevice(device);
            var rs1 = AdminService.AddDeviceToUser(user.UserID, rs, user.Username);
            return Json(new { success = true });
        }
        [HttpGet]
        public JsonResult GetListDeviceNotUsedByUser(int id) //id = UserID
        {
            var data = AdminService.GetListDeviceNotUsedByUser(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListDeviceBelongToAgencyNotUsedByUser() //id = UserID
        {
            //int AgencyID = UsersService.CheckUsers(Request.Cookies["Username"].Value.ToString(), Request.Cookies["Password"].Value.ToString()).RoleID;
            var us = (Users)Session["User"];
            var data = AdminService.GetListDeviceBelongToAgencyNotUsedByUser(us.UserID, us.Username);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetListDeviceOfCustomer(UserInfoDTO user) //id = UserID
        {
            //int AgencyID = UsersService.CheckUsers(Request.Cookies["Username"].Value.ToString(), Request.Cookies["Password"].Value.ToString()).RoleID;
            var us = (Users)Session["User"];
            var data = AdminService.GetListDeviceOfCustomer(user.ManageBy, user.UserID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RemoveDeviceFromUser(User_Device ud)
        {
            try
            {
                var rs = AdminService.RemoveDeviceFromUser(ud.UserID, ud.DeviceID);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });

                throw;
            }
        }

        [HttpPost]
        public JsonResult AddDeviceToUser(User_Device ud)
        {
            try
            {
                //string CreateBy = Request.Cookies["Username"].Value;
                var rs = AdminService.AddDeviceToUser(ud.UserID, ud.DeviceID, "Admin");
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });

                throw;
            }
        }
        [HttpPost]
        public ActionResult EditDevice(Device device)
        {


            var data = AdminService.UpdateDevice(device);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult DeleteDevice(int id)
        {
            var data = AdminService.DeleteDevice(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}