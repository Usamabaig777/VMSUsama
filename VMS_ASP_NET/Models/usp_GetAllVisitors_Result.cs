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
    
    public partial class usp_GetAllVisitors_Result
    {
        public int VisitorID { get; set; }
        public int SiteID { get; set; }
        public string VisitorSSN { get; set; }
        public string VisitorFirstName { get; set; }
        public string VisitorLastName { get; set; }
        public string VisitorAddress { get; set; }
        public string VisitorEmail { get; set; }
        public string Company { get; set; }
        public string PassNo { get; set; }
        public Nullable<int> VisitorStatusID { get; set; }
        public bool VisitorEntryExitNotification { get; set; }
        public string NotificationEmailAddress { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    }
}