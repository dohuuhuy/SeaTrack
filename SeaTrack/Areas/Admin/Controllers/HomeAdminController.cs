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
            if (!CheckRole(1))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult Customer()
        {
            if (!CheckRole(1))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            return View();
        }

        public new ActionResult User()
        {
            if (!CheckRole(1))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            return View();
        }

        [HttpGet]
        public ActionResult ListUser(int id) //id = roleID
        {
            if (!CheckRole(1))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            var rs = AdminService.GetListUser(id);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateUser(UserInfoDTO user, int roleID)
        {
            if (!CheckRole(1))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
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
            if (!CheckRole(1))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
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
            if (!CheckRole(1) && !CheckRole(2))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            var u = Session["User"] as Users;
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
                    if (u.RoleID==2)
                    {
                        return RedirectToAction("Detail", "Agency", new { id = us.UserID });
                    }
                    return RedirectToAction("Detail", "HomeAdmin",new { id = us.UserID });
                }
                else
                {
                    TempData["EditResult"] = "Chưa được cập nhật";
                    if (u.RoleID == 2)
                    {
                        return RedirectToAction("Detail", "Agency", new { id = us.UserID });
                    }
                    return RedirectToAction("Detail", "HomeAdmin", new { id = us.UserID });
                }
            }
            catch (Exception)
            {
                TempData["EditResult"] = "Xảy ra lỗi trong quá trình cập nhật";
                if (u.RoleID == 2)
                {
                    return RedirectToAction("Detail", "Agency", new { id = user.UserID });
                }
                return RedirectToAction("Detail", "HomeAdmin", new { id = user.UserID });
                throw;
            }
        }


        [HttpGet]
        public ActionResult LockUser(int id)
        {
            if (!CheckRole(1) && !CheckRole(2))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }

            var user = (Users)Session["User"];
            if (user.RoleID != 1)
            {
                var r = AdminService.GetUserByID(id).ManageBy == user.Username ? true : false;
                if (r || AdminService.CheckUserManage(id, user.Username))
                {
                    AdminService.UpdateStatusUser(id, -1);
                    return Json("Đã khóa", JsonRequestBehavior.AllowGet);
                }
                return Json("Không tìm thấy người dùng", JsonRequestBehavior.AllowGet);
            }
            AdminService.UpdateStatusUser(id, -1);
            return Json("Đã khóa", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UnLockUser(int id)
        {
            if (!CheckRole(1) && !CheckRole(2))
            {
                return RedirectToAction("ErrorView", "Home", new { area = "" });
            }
            var user = (Users)Session["User"];
            if (user.RoleID != 1)
            {
                var r = AdminService.GetUserByID(id).ManageBy == user.Username ? true : false;
                if (r || AdminService.CheckUserManage(id, user.Username))
                {
                    AdminService.UpdateStatusUser(id, 1);
                    return Json("Đã kích hoạt", JsonRequestBehavior.AllowGet);
                }
                return Json("Không tìm thấy người dùng", JsonRequestBehavior.AllowGet);
            }
            AdminService.UpdateStatusUser(id, 1);
            return Json("Đã kích hoạt", JsonRequestBehavior.AllowGet);
        }
        public bool CheckRole(int role)
        {
            var user = (Users)Session["User"];
            if (user != null && user.RoleID == role)
            {
                return true;
            }
            return false;
        }

    }
}