using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS_ASP_NET.Models.ViewModels
{
    public class CheckinsListVM
    {
        public int VisitMainID { get; set; }
        public int VisitorTypeID { get; set; }
        public string VisitorTypeDescription { get; set; }
        public DateTime CheckInTime { get; set; }
        public string CheckInGate { get; set; }
        public string VisitSummary { get; set; }
        public string VisitConeSummary { get; set; }
        public string VisitRegulatorySummary { get; set; }
        public string VisitEmergencySummary { get; set; }
    }
}