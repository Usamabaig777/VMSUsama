using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace VMS_ASP_NET.Models
{
    public class VisitorManagement
    {
        public DataTable VM_CheckedInVisits { get; set; }
        public DataTable VM_CheckoutsList { get; set; }
        public DataTable VM_GetVisitors { get; set; }
    }
}