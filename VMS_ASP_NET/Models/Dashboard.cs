using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace VMS_ASP_NET.Models
{
    public class Dashboard
    {
        public DataTable Dashboard_Tables { get; set; }
        public DataTable Dashboard_Stats_Tables { get; set; }
        public DataTable Dashboard_VisitorActivity_Tables { get; set; }
    }
}