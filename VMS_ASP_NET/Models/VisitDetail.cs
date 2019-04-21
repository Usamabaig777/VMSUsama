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
    
    public partial class VisitDetail
    {
        public int VisitDetailID { get; set; }
        public Nullable<int> CheckiInMainID { get; set; }
        public int VisitorID { get; set; }
        public Nullable<int> CheckOutMainID { get; set; }
        public System.DateTime CheckInTime { get; set; }
        public bool isManualCheckIn { get; set; }
        public Nullable<byte> isManualCheckOut { get; set; }
        public Nullable<byte> isDriverCheckOut { get; set; }
        public Nullable<System.DateTime> CheckOutTime { get; set; }
        public Nullable<byte> ProofOfInsurance { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Notes { get; set; }
        public Nullable<int> DestinationID { get; set; }
        public Nullable<byte> isDriverCheckIn { get; set; }
        public string CheckInSignature { get; set; }
        public Nullable<byte> OrientationVisit { get; set; }
    
        public virtual VisitMain VisitMain { get; set; }
    }
}
