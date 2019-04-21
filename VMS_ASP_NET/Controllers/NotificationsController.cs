using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;
using VMS_ASP_NET.Models.ViewModel;

namespace VMS_ASP_NET.Controllers
{
    public class NotificationsController : Controller
    {
        VMSFarhanEntities db = new VMSFarhanEntities();
        // GET: Notifications
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                Session["CurrentController"] = "Notifications";
                User user = (User)Session["User"];
                var sl = new SortedList<string, object>()
            {
                {"SiteID", Convert.ToInt32(Session["SiteId"]) },
                {"UserID", user.UserID}
            };
                var loDataTable = DataRepositoryControl.ExecuteTable("VMS_GetAllNotificationsDotNet", sl);
                Models.Notifications ViewModel = new Models.Notifications();
                ViewModel.Notification_Tables = loDataTable;

                int SiteID = Convert.ToInt32(Session["SiteId"]);
                var result = db.Database
                .SqlQuery<AllNotificationsVM>("VMS_GetAllNotificationsDotNet @SiteID, @UserID",
                new SqlParameter("@SiteID", SiteID),
                new SqlParameter("@UserID", user.UserID)
                ).ToList();


                Session["Notifications"] = result;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult NotificationList()
        {
            //Session["CurrentController"] = "Notification";
            User user = (User)Session["User"];

            int SiteID = Convert.ToInt32(Session["SiteId"]);
            var result = db.Database
            .SqlQuery<AllNotificationsVM>("VMS_GetAllNotificationsDotNet @SiteID, @UserID",
            new SqlParameter("@SiteID", SiteID),
            new SqlParameter("@UserID", user.UserID)
            ).ToList();


            Session["Notifications"] = result;

            return View("~/Views/Notifications/GetNotifications.cshtml");
        }

        public ActionResult ReadNotification(int id)
        {
            UserNotification notify = db.UserNotification.Find(id);
            notify.isRead = 1;
            TryUpdateModel(notify);
            db.SaveChanges();

            return RedirectToAction("");
        }
    }
}