using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace VMS_ASP_NET
{
    public class VisitorManagementController : Controller
    {
        // GET: VisitorManagement
        public ActionResult Index(string SiteID)
        {
            if (Session["User"] != null)
            {
                var sl = new SortedList<string, object>()
            {
                {"SiteID", 2 }
            };
                var loDataTable = DataRepositoryControl.ExecuteTable("vms_GetAllCheckedInVisits", sl);

                var sl2 = new SortedList<string, object>()
            {
                {"SiteID", 2 }
            };
                var loDataTable2 = DataRepositoryControl.ExecuteTable("vms_getTodaysAllCheckoutsList", sl2);

                var sl3 = new SortedList<string, object>()
            {
                {"SiteID", 2 }
            };
                var loDataTable3 = DataRepositoryControl.ExecuteTable("GetAllVisitors", sl3);


                Models.VisitorManagement ViewModel = new Models.VisitorManagement();
                ViewModel.VM_CheckedInVisits = loDataTable;
                ViewModel.VM_CheckoutsList = loDataTable2;
                ViewModel.VM_GetVisitors = loDataTable3;
                return View(ViewModel);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}