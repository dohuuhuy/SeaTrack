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
        public ActionResult GetListDeviceByUserID(int id)
        {
            var data = AdminService.GetListDeviceByUserID(id);
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
        [HttpGet]
        public JsonResult GetListDeviceNotUsedByUser(int id) //id = UserID
        {
            var data = AdminService.GetListDeviceNotUsedByUser(id);
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