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
    public class AgencyController : Controller
    {
        // GET: Admin/Agency
        public ActionResult Customer()
        {
            return View();
        }

        public new ActionResult User()
        {
            return View();
        }

        public ActionResult Device()
        {
            return View();
        }

        public ActionResult DeviceDetail()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            if (TempData["EditResult"] != null)
            {
                ViewBag.EditResult = TempData["EditResult"].ToString();
            }
            UserViewModel us = new UserViewModel();
            us = AdminService.GetUserByID(id);         
            if (us.RoleID == 4)
            {
                //string Username = Request.Cookies["Username"].Value.ToString();
                var user = (Users)Session["User"];
                List<UserViewModel> Customer = AdminService.GetListUserByUserID(user.Username, 3);
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var a in Customer)
                {
                    items.Add(new SelectListItem { Text = a.Username, Value = a.Username, Selected = a.ManageBy == us.ManageBy ? true : false });
                }
                ViewBag.ListCustomer = items;
            }

            return View(us);
        }

        [HttpGet]
        public JsonResult GetListUserByUserID(int id) //id = RoleID
        {
            //string Username = Request.Cookies["Username"].Value.ToString();
            var user = (Users)Session["User"];
            var rs = AdminService.GetListUserByUserID(user.Username, id);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListUserOfAgency() //id = RoleID
        {
            //string Username = Request.Cookies["Username"].Value.ToString();
            var user = (Users)Session["User"];
            var rs = AdminService.GetListUserOfAgency(user.Username);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

    }
}