using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using SeaTrack.Lib.DTO;
using SeaTrack.Models;
using System.Web.Mvc;
using SeaTrack.Lib.Service;

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
        public ActionResult GetListDevice(int id)  //id = -1 lấy tất cả thiết bị, id!= -1 lấy theo userID
        {
            var data = AdminService.GetListtDeviceByID(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetListDeviceStatus()
        {
            var data = TrackDataService.GetListDeviceStatus();
            return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
        }
       
    }
}