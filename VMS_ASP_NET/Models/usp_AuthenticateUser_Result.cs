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
    
    public partial class usp_AuthenticateUser_Result
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isLogin { get; set; }
        public int SiteID { get; set; }
        public int CompanyID { get; set; }
        public string SiteName { get; set; }
        public string Environment { get; set; }
        public bool isExternal { get; set; }
        public string RoleName { get; set; }
    }
}