using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMS_ASP_NET.Models
{
    public class CheckInVM
    {
        #region Vehicle Entity attributes

        public int VehicleTypeID { get; set; }

        [StringLength(50)]
        [DisplayName("License Plate Number")]
        public string LicensePlateNo { get; set; }

        public int? VehicleStateID { get; set; }

        public virtual VehicleMake Make { get; set; }
        public int MakeID { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Model Name")]
        public string ModelName { get; set; }

        public int? VehicleColorID { get; set; }

        [StringLength(50)]
        [DisplayName("Comments")]
        public string Comments { get; set; }

        #endregion

        #region VisitMain Entity Attribute

        public int VisitorTypeID { get; set; }

        public int SiteID { get; set; }

        public DateTime CheckInMainTime { get; set; }

        [StringLength(30)]
        public string CheckInDevice { get; set; }

        public int CheckInMainUserID { get; set; }

        public int CheckInMainGateID { get; set; }

        public int? CheckInMainTrailerID { get; set; }

        public DateTime? CheckOutTime { get; set; }

        [StringLength(30)]
        public string CheckOutDevice { get; set; }

        public int? CheckOutMainGateID { get; set; }

        public int? CheckOutMainUserID { get; set; }

        public int? CheckOutMainTrailerID { get; set; }

        [StringLength(25)]
        [DisplayName("Placard Number")]
        public string PlacardNo { get; set; }

        [DisplayName("Vehicle Search Consent")]
        public byte? VehicleSearchConsent { get; set; }

        [DisplayName("Unit Number")]
        public int? UnitNo { get; set; }

        [DisplayName("Section Sector")]
        public int? SectionSector { get; set; }

        [StringLength(50)]
        [DisplayName("Emergency Vehicle Type")]
        public string EmergencyVehicleType { get; set; }

        [StringLength(50)]
        [DisplayName("Location")]
        public string Location { get; set; }

        [StringLength(50)]
        [DisplayName("Organization")]
        public string Organization { get; set; }

        [StringLength(25)]
        [DisplayName("Pass Number")]
        public string PassNumber { get; set; }

        [StringLength(25)]
        [DisplayName("Cone Number")]
        public string ConeNumber { get; set; }

        public int? ConeColorID { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public int? VehicleID { get; set; }

        [StringLength(50)]
        [DisplayName("Vehicle Search Result")]
        public string VehicleSearchResult { get; set; }
        #endregion

        #region VisitDetail Entity  Attributes

        public int? CheckiInMainID { get; set; }

        public virtual Visitor Visitor { get; set; }
        public int VisitorID { get; set; }

        public int? CheckOutMainID { get; set; }

        public DateTime CheckInTime { get; set; }

        public bool isManualCheckIn { get; set; }

        public byte? isManualCheckOut { get; set; }

        public byte? isDriverCheckOut { get; set; }

        //public DateTime? CheckOutTime { get; set; }

        [DisplayName("Proof of Insurance")]
        public byte? ProofOfInsurance { get; set; }

        [StringLength(50)]
        [DisplayName("Notes")]
        public string Notes { get; set; }

        public int? DestinationID { get; set; }

        [DisplayName("Driver")]
        public byte? isDriverCheckIn { get; set; }

        [DisplayName("Check-in Signature")]
        public string CheckInSignature { get; set; }

        [DisplayName("Orientation Employee")]
        public byte? OrientationVisit { get; set; }

        #endregion

        #region Visitor's Entity Attributes

        [Required]
        [StringLength(4)]
        [DisplayName("ID")]
        public string VisitorSSN { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("First Name")]
        public string VisitorFirstName { get; set; }

        [StringLength(30)]
        [DisplayName("Last Name")]
        public string VisitorLastName { get; set; }

        [StringLength(100)]
        [DisplayName("Address")]
        public string VisitorAddress { get; set; }

        [StringLength(100)]
        [DisplayName("Email")]
        public string VisitorEmail { get; set; }

        [Required]
        [DisplayName("Company")]
        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(25)]
        [DisplayName("Pass Number")]
        public string PassNo { get; set; }

        public int? VisitorStatusID { get; set; }

        public bool VisitorEntryExitNotification { get; set; }

        [StringLength(50)]
        public string NotificationEmailAddress { get; set; }

        #endregion

        #region Trailer Entity Attributed
        public int TrailerAttachmentID { get; set; }

        public int TrailerStateID { get; set; }

        public int TrailerTypeID { get; set; }

        [StringLength(50)]
        [DisplayName("TrailerPlateNo")]
        public string TrailerPlateNo { get; set; }

        [StringLength(50)]
        [DisplayName("Trailer VIN")]
        public string TrailerVIN { get; set; }
        #endregion

        #region Gates Entity Attributes
        public int GateID { get; set; }
        public string GateDescription { get; set; }
        #endregion

        #region StateProvince Entity attributes
        public int StateID { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        #endregion

        #region Color Entity attributes
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        #endregion

        #region VisitorType Entity attributes
        public string VisitorTypeDescription { get; set; }
        public string VisitorTypeName { get; set; }
        #endregion

        #region Make Entity attributes
        public string MakeName { get; set; }
        #endregion

        #region VisitorType Entity attributes
        public string VehicleTypeName { get; set; }
        #endregion
    }
}
