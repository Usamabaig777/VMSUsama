//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VMS_ASP_NET.Models
{
    using System;
    
    public partial class VMS_GetAllNotifications_Result
    {
        public int NotificationID { get; set; }
        public string NotificationHeader { get; set; }
        public string NotificationDescription { get; set; }
        public int SiteID { get; set; }
        public string SenderName { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string TimeAgo { get; set; }
    }
}
