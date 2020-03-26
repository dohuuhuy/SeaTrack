using SeaTrack.Lib.DTO.Admin;
using SeaTrack.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaTrack.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            //yêu cầu check role
            return View("Index");
        }

        [HttpGet]
        public JsonResult ListUser(int id) //id = roleID
        {
            //if (!CheckRole(1))
            //{
            //    return Json("error", JsonRequestBehavior.AllowGet);
            //}
            var rs = AdminService.GetListUser(id);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUser(UserInfoDTO user, int roleID)
        {
            try
            {
                user.CreateBy = Request.Cookies["userName"].Value.ToString();
                user.CreateDate = DateTime.Now;
                var rs = AdminService.CreateUser(user, roleID);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            //if (!CheckRole(1))
            //{
            //    return RedirectToAction("Login", "Home", new { area = "" });
            //}
            if (TempData["EditResult"] != null)
            {
                ViewBag.EditResult = TempData["EditResult"].ToString();
            }
            UserInfoDTO us = new UserInfoDTO();
            us = AdminService.GetUserByID(id);
            return View(us);
        }

        [HttpPost]
        public ActionResult EditUser(UserInfoDTO user)
        {
            try
            {
                UserInfoDTO us = AdminService.GetUserByID(user.UserID);
                us.Username = user.Username;
                us.Password = user.Password;
                us.Fullname = user.Fullname;
                us.Phone = user.Phone;
                us.Address = user.Address;
                us.Status = user.Status;
                us.UpdateBy = "admin";
                us.LastUpdateDate = DateTime.Now;
                bool res = AdminService.EditUser(us);
                if (res)
                {
                    TempData["EditResult"] = "Cập nhật thành công";
                    return RedirectToAction("EditUser", us);
                }
                else
                {
                    TempData["EditResult"] = "Chưa được cập nhật";
                    return RedirectToAction("EditUser", us);
                }
            }
            catch (Exception)
            {
                TempData["EditResult"] = "Xảy ra lỗi trong quá trình cập nhật";
                return RedirectToAction("EditUser", user);
                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            bool res = AdminService.DeleteUser(id);
            if (res)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }


        public bool CheckRole(int role)
        {
            if (Request.Cookies["userName"] != null && Request.Cookies["pass"] != null && Request.Cookies["role"].Value == role.ToString())
            {
                return true;
            }
            return false;
        }

    }
}