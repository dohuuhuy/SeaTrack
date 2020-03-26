using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SeaTrack.Lib.DTO;
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
        public ActionResult ActionResult ()
        {
            return View();
        }
        //[HttpGet]
        //public HttpResponseMessage listdevice(int )
        //{
        //    var rs = AdminService.GetListUser(RoleID);
        //    return Request.CreateResponse(HttpStatusCode.OK, new
        //    {
        //        STATUSCODE = rs != null ? Util.Static.SUCCESS_CODE : Util.Static.ERROR_CODE,
        //        DATA = rs
        //    });
        //}
    }
}