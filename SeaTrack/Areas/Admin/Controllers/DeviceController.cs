﻿using SeaTrack.Lib.DTO;
using SeaTrack.Lib.Service;

using System.Web.Http.Results;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;

using SeaTrack.Models;
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
        public ActionResult GetListDeviceByID(int deviceID)
        {
            var data = AdminService.GetListDeviceByID(deviceID);
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
            try
            {
                var rs = AdminService.CreateDevice(device);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

     

        //[HttpPost]
        //public ActionResult Editdevice(Device device, int deviceID)
        //{
        //    try
        //    {
        //        Device tb = AdminService.GetListDeviceByID(deviceID);
        //        //tb.devicename = device.devicename;
         
        //        bool res = AdminService.UpdateDevice(tb);
        //        if (res)
        //        {
        //            TempData["EditResult"] = "Cập nhật thành công";
        //            return RedirectToAction("Editdevice", tb);
        //        }
        //        else
        //        {
        //            TempData["EditResult"] = "Chưa được cập nhật";
        //            return RedirectToAction("Editdevice", tb);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        TempData["EditResult"] = "Xảy ra lỗi trong quá trình cập nhật";
        //        return RedirectToAction("Editdevice", device);
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public JsonResult Deletedevice(int id)
        //{
        //    bool res = AdminService.Deletedevice(id);
        //    if (res)
        //    {
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false });

        //}





    }
}