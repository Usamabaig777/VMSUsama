using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VMS_ASP_NET.Models.ViewModel
{
    public class VisitorDetailID
    {
        public string VisitorName { get; set; }
        public string SiteName { get; set; }
        
        public string CheckInDate { get; set; }
        public string CheckInTime { get; set; }
        public string CheckInFromGate { get; set; }
        public string CheckedInByUser { get; set; }
        //public DateTime CheckInTime { get; set; }
        public string DestinationDescription { get; set; }
        public string CheckInLicensePlateNo { get; set; }
        public string CheckInStateName { get; set; }
        public string CheckInVehicleTypeName { get; set; }
        public string CheckInColorName { get; set; }
        public string CheckInMakeName { get; set; }
        public string CheckInModelName { get; set; }
        // Checkout details
        public string CheckOutDate { get; set; }
        public string CheckOutTime { get; set; }
        public string CheckOutFromGate { get; set; }
        public string CheckedOutByUser { get; set; }
        public string CheckOutLicensePlateNo { get; set; }
        public string CheckOutStateName { get; set; }
        public string CheckOutVehicleTypeName { get; set; }
        public string CheckOutColorName { get; set; }
        public string CheckOutMakeName { get; set; }
        public string CheckOutModelName { get; set; }

    }
}