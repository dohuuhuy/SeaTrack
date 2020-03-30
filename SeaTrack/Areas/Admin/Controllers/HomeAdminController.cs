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
        public ActionResult Index() //Quản lý đại lý
        {
            //yêu cầu check role
            return View();
        }

        public ActionResult Customer()
        {
            return View();
        }

        public new ActionResult User()
        {
            return View();
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

            user.CreateBy = Request.Cookies["userName"].Value.ToString();
            user.CreateDate = DateTime.Now;
            user.UpdateBy = "admin";
            user.LastUpdateDate = DateTime.Now;
            user.Status = 1;
            var rs = AdminService.CreateUser(user, roleID);
            return Json(new { success = true });

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
            if (us.RoleID == 3)
            {
                List<UserInfoDTO> Agencys = AdminService.GetListUser(2);
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var a in Agencys)
                {
                    items.Add(new SelectListItem { Text = a.Username, Value = a.Username, Selected = a.ManageBy == us.ManageBy ? true : false });
                }
                ViewBag.ListAgencys = items;
            }
            if (us.RoleID == 4)
            {
                List<UserInfoDTO> Customer = AdminService.GetListUser(3);
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var a in Customer)
                {
                    items.Add(new SelectListItem { Text = a.Username, Value = a.Username, Selected = a.ManageBy == us.ManageBy ? true : false });
                }
                ViewBag.ListCustomer = items;
            }

            return View(us);
        }

        [HttpPost]
        public ActionResult EditUser(UserInfoDTO user)
        {
            try
            {
                UserInfoDTO us = AdminService.GetUserByID(user.UserID);
                us.Password = user.Password;
                us.Fullname = user.Fullname;
                us.Phone = user.Phone;
                us.Address = user.Address;
                us.UpdateBy = "admin";
                us.ManageBy = user.ManageBy;
                us.LastUpdateDate = DateTime.Now;
                bool res = AdminService.EditUser(us);
                if (res)
                {
                    TempData["EditResult"] = "Cập nhật thành công";
                    return RedirectToAction("Detail", new { id = us.UserID });
                }
                else
                {
                    TempData["EditResult"] = "Chưa được cập nhật";
                    return RedirectToAction("Detail", new { id = us.UserID });
                }
            }
            catch (Exception)
            {
                TempData["EditResult"] = "Xảy ra lỗi trong quá trình cập nhật";
                return RedirectToAction("Detail", new { id = user.UserID });
                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            bool res = AdminService.DeleteUser(id);
            if (res)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult LockUser(int id)
        {
            bool res = AdminService.UpdateStatusUser(id, -1);
            if (res)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UnLockUser(int id)
        {
            bool res = AdminService.UpdateStatusUser(id, 1);
            if (res)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
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