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
    using System.Collections.Generic;
    
    public partial class UserNotification
    {
        public int UserNotificationID { get; set; }
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public byte isRead { get; set; }
    
        public virtual Notification Notification { get; set; }
        public virtual User User { get; set; }
    }
}
