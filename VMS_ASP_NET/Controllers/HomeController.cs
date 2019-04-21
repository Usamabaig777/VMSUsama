using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;

namespace VMS_ASP_NET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["CurrentController"] = "Home";
            if (Session["User"]!=null)
            {
                Session["ErrorMessage"] = null;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Header()
        {
            return View();
        }

        public ActionResult SiderBar()
        {
            return View();
        }
    }
}