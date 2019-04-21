using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS_ASP_NET.Models.ViewModel
{
    public class AllNotificationsVM
    {
        public int NotificationID { get; set; }
        public string NotificationHeader { get; set; }
        public string NotificationDescription { get; set; }
        public int SiteID { get; set; }
        public int UserID { get; set; }
        public int UserNotificationID { get; set; }
        public string SenderName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TimeAgo { get; set; }
        public Byte isRead { get; set; }
    }
}