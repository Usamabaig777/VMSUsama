using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS_ASP_NET.Models.ViewModels
{
    public class FrequentVisitorsList
    {
        public int    VisitorID { get; set; }
        public string VisitorSSN { get; set; }
        public string VisitorFirstName { get; set; }
        public string VisitorLastName { get; set; }
        public string Company { get; set; }
        public string DestinationDescription { get; set; }
        public string VisitorStatusDescription { get; set; }
    }
}