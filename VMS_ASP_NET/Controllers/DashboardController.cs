using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using VMS_ASP_NET.Models;
using VMS_ASP_NET.Models.ViewModel;
using System.Data.SqlClient;

namespace VMS_ASP_NET.Controllers
{
    public class DashboardController : Controller
    {
        VMSFarhanEntities db = new VMSFarhanEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {

                Session["CurrentController"] = "Dashboard";

                Session["ErrorMessage"] = null;

                int siteID = (int)Session["SiteId"];
                Site site = db.Site.Find(siteID);
                Session["SiteObject"] = site;

                var sl = new SortedList<string, object>()
            {
                {"SiteID", siteID }
            };
                var loDataTable = DataRepositoryControl.ExecuteTable("GetDashboardStats", sl);

                var sl2 = new SortedList<string, object>()
            {
                {"SiteID", siteID }
            };
                var loDataTable2 = DataRepositoryControl.ExecuteTable("usp_VisitorActivityFeed", sl2);
                var sl3 = new SortedList<string, object>()
            {
                {"VisitDetailID", 312 }
            };
                var loDataTable3 = DataRepositoryControl.ExecuteTable("vms_DotNet_GetVisitorDetailByVisitDetailID", sl3);


                Models.Dashboard ViewModel = new Models.Dashboard();
                ViewModel.Dashboard_Stats_Tables = loDataTable;
                ViewModel.Dashboard_VisitorActivity_Tables = loDataTable2;
                ViewModel.Dashboard_Tables = loDataTable3;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetVisitorDetail(int id)
        {
            var result = db.Database
            .SqlQuery<VisitorDetailID>("vms_DotNet_GetVisitorDetailByVisitDetailID @VisitDetailID",
            new SqlParameter("@VisitDetailID", id)
            ).SingleOrDefault();


            Session["VisitDetail"] = result;

            return View("~/Views/Dashboard/_VisitorDetail.cshtml");
        }
    }
}