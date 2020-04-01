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

            //user.CreateBy = Request.Cookies["userName"].Value.ToString();
            var us = (Users)Session["User"];
            user.CreateBy = us.Username;
            user.CreateDate = DateTime.Now;
            user.UpdateBy = "";
            user.LastUpdateDate = DateTime.Now;
            user.Status = 1;
            if (user.ManageBy==null)
            {
                user.ManageBy = us.Username;
            }
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
            UserViewModel us = new UserViewModel();
            us = AdminService.GetUserByID(id);
            return View(us);
        }

        [HttpPost]
        public ActionResult EditUser(UserInfoDTO user)
        {
            try
            {
                UserViewModel us = AdminService.GetUserByID(user.UserID);
                UserInfoDTO UserEdit = new UserInfoDTO();
                UserEdit.UserID = us.UserID;
                UserEdit.Username = us.Username;
                UserEdit.Status = us.Status;
                UserEdit.CreateBy = us.CreateBy;
                UserEdit.CreateDate = Convert.ToDateTime(us.CreateDate);
                UserEdit.RoleID = user.RoleID;
                UserEdit.Password = user.Password;
                UserEdit.Fullname = user.Fullname;
                UserEdit.Phone = user.Phone;
                UserEdit.Address = user.Address;
                UserEdit.UpdateBy = "admin";
                UserEdit.ManageBy = user.ManageBy;
                UserEdit.LastUpdateDate = DateTime.Now;
                bool res = AdminService.EditUser(UserEdit);
                if (res)
                {
                    TempData["EditResult"] = "Cập nhật thành công";
                    var u = Session["User"] as Users;
                    if (u.RoleID==2)
                    {
                        return RedirectToAction("Detail", "Agency", new { id = us.UserID });
                    }
                    return RedirectToAction("Detail", new { id = us.UserID });
                }
                else
                {
                    TempData["EditResult"] = "Chưa được cập nhật";
                    var u = Session["User"] as Users;
                    if (u.RoleID == 2)
                    {
                        return RedirectToAction("Detail", "Agency", new { id = us.UserID });
                    }
                    return RedirectToAction("Detail", new { id = us.UserID });
                }
            }
            catch (Exception)
            {
                TempData["EditResult"] = "Xảy ra lỗi trong quá trình cập nhật";
                var u = Session["User"] as Users;
                if (u.RoleID == 2)
                {
                    return RedirectToAction("Detail", "Agency", new { id = user.UserID });
                }
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