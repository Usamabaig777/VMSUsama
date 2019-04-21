using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;

namespace VMS_ASP_NET.Controllers
{
    public class TestController : Controller
    {
        VMSFarhanEntities db = new VMSFarhanEntities();
        // GET: Test
        public ActionResult MultiSelect()
        {
            return View();
        }
        public ActionResult Users(int? page)
        {
            UserRole userRole = (UserRole)Session["userRole"];
            int siteID = 1;

            var userList = db.UserRole.Where(m => m.User.SiteID == siteID).OrderByDescending(m=>m.UserID).ToPagedList(page ?? 1, 4);
            ViewBag.ListofUsers = userList;

            return View(userList);

        }
    }
}