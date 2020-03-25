using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using SeaTrack.Lib.DTO;
using SeaTrack.Lib.DTO.Admin;
using SeaTrack.Lib.Service;
using SeaTrack.Models;
using SeaTrack.Providers;
using SeaTrack.Results;
using SeaTrack.Lib;

namespace SeaTrack.Controllers
{
    [Authorize]
    public class AdminController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody]UserInfoDTO user)
        {
            var rs = AdminService.CreateUser(user, user.RoleID);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                STATUSCODE = rs > 0 ? Util.Static.SUCCESS_CODE : Util.Static.ERROR_CODE,
            });
        }
        [HttpGet]
        public HttpResponseMessage ListUser(int RoleID)
        {
            var rs = AdminService.GetListUser(RoleID);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                STATUSCODE = rs != null ? Util.Static.SUCCESS_CODE : Util.Static.ERROR_CODE,
                DATA = rs
            });
        }
        [HttpPost]
        public HttpResponseMessage UpdateUser([FromBody]UserInfoDTO user)
        {
            var rs = AdminService.UpdateUser(user);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                STATUSCODE = rs != null ? Util.Static.SUCCESS_CODE : Util.Static.ERROR_CODE,
                DATA = rs
            });
        }

        #region Device
        [HttpPost]
        public HttpResponseMessage CreateDevice([FromBody]Device device)
        {
            var rs = AdminService.CreateDevice(device);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                STATUSCODE = rs > 0 ? Util.Static.SUCCESS_CODE : Util.Static.ERROR_CODE,
            });
        }
        [HttpPost]
        public HttpResponseMessage UpdateDevice([FromBody]Device device)
        {
            var rs = AdminService.UpdateDevice(device);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                STATUSCODE = rs > 0 ? Util.Static.SUCCESS_CODE : Util.Static.ERROR_CODE,
            });
        }
        [HttpGet]
        public HttpResponseMessage ListDevice()
        {
            var rs = AdminService.GetListDevice();
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                STATUSCODE = rs != null ? Util.Static.SUCCESS_CODE : Util.Static.ERROR_CODE,
                DATA = rs
            });
        }
        #endregion
    }
}
