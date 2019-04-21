using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS_ASP_NET.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SiteID { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public int ModifiedBy { get; set; }
        //public DateTime ModifiedOn { get; set; }
        public bool isActive { get; set; }
        public bool isLogin { get; set; }
        public bool isPasswordReset { get; set; }
        public bool isExternal { get; set; }

        public int UserRoleID { get; set; }
        public int RoleID { get; set; }
        
        public int CompanyID { get; set; }
        public string RoleName { get; set; }

    }
}