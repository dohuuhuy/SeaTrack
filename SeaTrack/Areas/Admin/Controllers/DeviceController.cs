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
            if (CheckRole(1) == -1)
            {
                if (CheckRole(1) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            return View();
        }
        //public ActionResult Detail(int id)
        //{
        //    if (CheckRole(1) == -1)
        //    {
        //        if (CheckRole(1) == 0)
        //        {
        //            return RedirectToAction("Login", "Home", new { area = "" });
        //        }
        //        return RedirectToAction("ErrorView", "Home", new { area = "" });
        //    }

        //    var device = AdminService.GetDeviceByID(id);
        //    return View(device);
        //}

        [HttpPost]
        public ActionResult Update(DeviceViewModel device)
        {
            if (CheckRole(1) == -1)
            {
                if (CheckRole(1) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

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
        [HttpGet]
        public ActionResult GetListDevice()
        {
            if (CheckRole(1) == -1)
            {
                if (CheckRole(1) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
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
            if (CheckRole(1) == -1  && CheckRole(2) == -1)
            {
                if (CheckRole(1) == 0 && CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

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
        public ActionResult CreateDevice(Device device)
        {
            if (CheckRole(1) == -1)
            {
                if (CheckRole(1) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            var user = (Users)Session["User"];
            device.CreateBy = user.Username;
            device.DateCreate = DateTime.Now;
            device.StatusDevice = 1;
            var rs = AdminService.CreateDevice(device);
            return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult AgencyCreateDevice(Device device)
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
            device.CreateBy = user.Username;
            device.DateCreate = DateTime.Now;
            device.StatusDevice = 1;
            var rs = AdminService.CreateDevice(device);
            var rs1 = AdminService.AddDeviceToUser(user.UserID, rs, user.Username);
            return Json(new { success = true });
        }

        //Username != null, Lấy danh sách thiết bị thuộc về UserID nhưng chưa được gán cho người dùng khác
        //Username == null, lấy danh sách thiết bị chưa được gán cho bất kỳ người dùng
        [HttpGet]
        public ActionResult GetListDeviceNotUsedByUser(string Username)
        {
            if (CheckRole(1) == -1)
            {
                if (CheckRole(1) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            var data = AdminService.GetListDeviceNotUsedByUser(Username);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetListDeviceBelongToAgencyNotUsedByUser() //id = UserID
        {
            if (CheckRole(2) == -1)
            {
                if (CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            var us = (Users)Session["User"];
            var data = AdminService.GetListDeviceBelongToAgencyNotUsedByUser(us.UserID, us.Username);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetListDeviceOfCustomer(UserInfoDTO user) //id = UserID
        {
            if (CheckRole(1) == -1 && CheckRole(2) == -1)
            {
                if (CheckRole(1) == 0 && CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            var data = AdminService.GetListDeviceOfCustomer(user.ManageBy, user.UserID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveDeviceFromUser(User_Device ud)
        {
            if (CheckRole(1) == -1 && CheckRole(2) == -1)
            {
                if (CheckRole(1) == 0 && CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
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
        public ActionResult AddDeviceToUser(User_Device ud)
        {
            if (CheckRole(1) == -1 && CheckRole(2) == -1)
            {
                if (CheckRole(1) == 0 && CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

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
            if (CheckRole(1) == -1 && CheckRole(2) == -1)
            {
                if (CheckRole(1) == 0 && CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            var user = (Users)Session["User"];
            if (user.RoleID != 1)
            {
                var res = AdminService.CheckUserDevice(user.UserID, device.DeviceID);
                if (res)
                {
                    AdminService.UpdateDevice(device);
                    return Json("Đã cập nhật", JsonRequestBehavior.AllowGet);
                }
                return Json("Không tìm thấy thiết bị", JsonRequestBehavior.AllowGet);
            }
            var data = AdminService.UpdateDevice(device);
            return Json("Đã cập nhật", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult DeleteDevice(int id)
        {
            if (CheckRole(1) == -1 && CheckRole(2) == -1)
            {
                if (CheckRole(1) == 0 && CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            var user = (Users)Session["User"];
            if (user.RoleID != 1)
            {
                var res = AdminService.CheckUserDevice(user.UserID, id);
                if (res)
                {
                    AdminService.DeleteDevice(id);
                    return Json("Đã khóa", JsonRequestBehavior.AllowGet);
                }
                return Json("Không tìm thấy thiết bị", JsonRequestBehavior.AllowGet);
            }
            var data = AdminService.DeleteDevice(id);
            return Json("đã khóa", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UnlockDevice(int id)
        {
            if (CheckRole(1) == -1 && CheckRole(2) == -1)
            {
                if (CheckRole(1) == 0 && CheckRole(2) == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            var user = (Users)Session["User"];
            if (user.RoleID != 1)
            {
                var res = AdminService.CheckUserDevice(user.UserID, id);
                if (res)
                {
                    AdminService.UnlockDevice(id);
                    return Json("Đã kích hoạt", JsonRequestBehavior.AllowGet);
                }
                return Json("Không tìm thấy thiết bị", JsonRequestBehavior.AllowGet);
            }
            var data = AdminService.DeleteDevice(id);
            return Json("Đã kích hoạt", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckDeviceExist(Device device)
        {
            Device d = new Device();
            if (device.DeviceNo != null)
            {
                var res = AdminService.CheckDeviceExist(device.DeviceNo, device.DeviceImei);
                d.DeviceNo = "OK";
                d.DeviceNo = res;
                return Json(d, JsonRequestBehavior.AllowGet);
            }
            if (device.DeviceImei != null)
            {
                var res = AdminService.CheckDeviceExist(device.DeviceNo, device.DeviceImei);
                d.DeviceNo = "OK";
                d.DeviceImei = res;
                return Json(d, JsonRequestBehavior.AllowGet);
            }
            d.DeviceNo = "OK";
            d.DeviceImei = "OK";
            return Json(d, JsonRequestBehavior.AllowGet);
        }
        public int CheckRole(int role)
        {
            var user = (Users)Session["User"];
            if (user != null)
            {
                if (user.RoleID != role)
                {
                    return -1; //sai quyền
                }
                return 1; //Hợp lệ
            }
            return 0; //Chưa đăng nhập
        }

    }
}