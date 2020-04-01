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
        public ActionResult Detail(int id)
        {
            var device = AdminService.GetDeviceByID(id);
            return View(device);
        }

        [HttpPost]
        public ActionResult Update(DeviceViewModel device)
        {
            Device dv = new Lib.DTO.Device();
            dv.DeviceID = device.DeviceID;
            dv.DeviceNo = device.DeviceNo;
            dv.DeviceName = device.DeviceName;
            dv.DeviceVersion = device.DeviceVersion;
            dv.DeviceImei = device.DeviceImei;
            dv.DeviceGroup = device.DeviceGroup;
            dv.DeviceNote = device.DeviceNote;
            dv.DateExpired = device.ExpireDate;
            var res = AdminService.UpdateDevice(dv);
            return RedirectToAction("Detail", new { id = dv.DeviceID });
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
        public ActionResult GetListDeviceByUserID(int? id)
        {
            if (id == null)
            {
                var user = (Users)Session["User"];
                var data = AdminService.GetListDeviceByUserID(user.UserID);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = AdminService.GetListDeviceByUserID((int)id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

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

        //UserID != null, Lấy danh sách thiết bị thuộc về UserID nhưng chưa được gán cho người dùng khác
        //UserID == null, lấy danh sách thiết bị chưa được gán cho bất kỳ người dùng
        [HttpGet]
        public JsonResult GetListDeviceNotUsedByUser(string Username) //id = UserID
        {
            var data = AdminService.GetListDeviceNotUsedByUser(Username);
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