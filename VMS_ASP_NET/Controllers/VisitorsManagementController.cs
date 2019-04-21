using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;
using VMS_ASP_NET.Models.ViewModel;
using VMS_ASP_NET.Models.ViewModels;

namespace VMS_ASP_NET.Controllers
{
    public class VisitorsManagementController : Controller
    {
        VMSFarhanEntities db = new VMSFarhanEntities();
        public ActionResult Index()
        {
            Session["VisitorDetail"] = null;

            if (Session["User"] != null)
            {
                //var getUserzzList=db.VMS_GetAllUsers(1,1);
                List<VisitorType> visitorType = db.VisitorType.ToList();
                ViewBag.VisitorTypes = new SelectList(visitorType, "VisitorTypeId", "VisitorTypeDescription");
                List<Gate> gate = db.Gate.ToList();
                ViewBag.Gates = new SelectList(gate, "GateID", "GateDescription");
                List<Color> colors = db.Color.ToList();
                ViewBag.Colors = new SelectList(colors, "ColorID", "ColorName");
                ViewBag.Status = new SelectList(db.VisitorStatu.ToList(), "VisitorStatusID", "VisitorStatusDescription");
                ViewBag.VehicleType = new SelectList(db.VehicleType.ToList(), "VehicleTypeID", "VehicleTypeName");
                ViewBag.VehicleState = new SelectList(db.StateProvince.ToList(), "StateID", "StateName");
                ViewBag.Make = new SelectList(db.VehicleMake.ToList(), "MakeID", "MakeName");
                //ViewBag.VehicleLicensePlate = new SelectList(db.Vehicles.ToList(), "VehicleID", "LicensePlateNo");
                
                ViewBag.TrailerTypeList = new SelectList(db.TrailerType.AsNoTracking().ToList(), "TrailerTypeID", "TrailerTypeDescription");
                ViewBag.TrailerStatesList = new SelectList(db.StateProvince.AsNoTracking().ToList(), "StateID", "StateName");
                ViewBag.TrailerAttachment = new SelectList(db.TrailerAttachment.AsNoTracking().ToList(), "TrailerAttachmentID", "TrailerAttachmentName");
                

                //db.Database.SqlQuery<>
                var SiteID = new SqlParameter("@SiteID", 1);
                ViewBag.getAllCheckInVisits = db.Database
                .SqlQuery<CheckinsListVM>("vms_GetAllCheckedInVisitsDotNet @SiteID", SiteID)
                .ToList();

                return View();
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

            
        }
        [HttpPost]
        public ActionResult CreateRegularCheckIn(CheckInVM checkInVM, Vehicle vehicle, Visitor visitor)
        {
            if (checkInVM.VisitorTypeID == 1)
            {
                VisitMain visit = new VisitMain()
                {
                    ConeNumber = checkInVM.ConeNumber,
                    ConeColorID = checkInVM.ConeColorID,
                    VisitorTypeID = checkInVM.VisitorTypeID,
                    CheckInMainGateID = checkInVM.GateID,
                    CheckInMainTime = DateTime.Now,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CheckInMainUserID = 1, // CheckInMainUserID is user's id who is doing checkin
                    SiteID = 1 // SiteID is SiteID from where visitor is doing checkin
                };
                db.VisitMain.Add(visit);
                db.SaveChanges();
            }

            if (checkInVM.VisitorTypeID == 2)
            {
                VisitMain visit = new VisitMain()
                {
                    CheckInMainTime = DateTime.Now,
                    Location = checkInVM.Location,
                    PassNumber = checkInVM.PassNo,
                    Organization = checkInVM.Organization,
                    CheckInMainGateID = checkInVM.GateID,
                    VisitorTypeID = checkInVM.VisitorTypeID,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CheckInMainUserID = 1, // CheckInMainUserID is user's id who is doing checkin
                    SiteID = 1 // SiteID is SiteID from where visitor is doing checkin
                };
                db.VisitMain.Add(visit);
                db.SaveChanges();
            }

            if (checkInVM.VisitorTypeID == 3)
            {
                VisitMain visit = new VisitMain()
                {
                    CheckInMainTime = DateTime.Now,
                    UnitNo = checkInVM.UnitNo,
                    SectionSector = checkInVM.SectionSector,
                    EmergencyVehicleType = checkInVM.EmergencyVehicleType,
                    CheckInMainGateID = checkInVM.GateID,
                    VisitorTypeID = checkInVM.VisitorTypeID,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CheckInMainUserID = 1, // CheckInMainUserID is user's id who is doing checkin
                    SiteID = 1 // SiteID is SiteID from where visitor is doing checkin
                };
                db.VisitMain.Add(visit);
                db.SaveChanges();
            }

            if (checkInVM.VisitorTypeID == 4)
            {
                if (vehicle.VehicleID == 0)
                {
                    vehicle.CreatedOn = DateTime.Now;
                    vehicle.ModifiedOn = DateTime.Now;
                    vehicle.CreatedBy = 1;
                    vehicle.ModifiedBy = 1;
                    //vehicle.VehicleColorID = checkInVM.ColorID;
                    //vehicle.VehicleStateID = checkInVM.StateID;
                    db.Vehicle.Add(vehicle);
                    db.SaveChanges();
                }

                VisitMain visitMain = new VisitMain()
                {
                    VehicleID = vehicle.VehicleID,
                    VisitorTypeID = checkInVM.VisitorTypeID,
                    CheckInMainGateID = checkInVM.GateID,
                    CheckInMainTime = DateTime.Now,
                    PlacardNo = checkInVM.PlacardNo,
                    VehicleSearchConsent = checkInVM.VehicleSearchConsent,
                    VehicleSearchResult = checkInVM.VehicleSearchResult,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CheckInMainUserID = 1, // CheckInMainUserID is user's id who is doing checkin
                    SiteID = 1 // SiteID is SiteID from where visitor is doing checkin
                };
                db.VisitMain.Add(visitMain);
                db.SaveChanges();

                VisitDetail visit = new VisitDetail()
                {
                    VisitorID = visitor.VisitorID,
                    CheckInSignature = checkInVM.CheckInSignature,
                    CheckiInMainID = visitMain.VisitMainID,
                    ProofOfInsurance = checkInVM.ProofOfInsurance,
                    isManualCheckIn = checkInVM.isManualCheckIn,
                    isDriverCheckOut = checkInVM.isDriverCheckOut,
                    isManualCheckOut = checkInVM.isManualCheckOut,
                    DestinationID = checkInVM.DestinationID,
                    isDriverCheckIn = checkInVM.isDriverCheckIn,
                    OrientationVisit = checkInVM.OrientationVisit,
                    CheckInTime = DateTime.UtcNow,
                    Notes = checkInVM.Notes,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                };
                db.VisitDetail.Add(visit);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult fbGetVehicleData(int VehicleID)
        {
            var getVehicleRelatedData = (from Veh in db.Vehicle.AsNoTracking().AsNoTracking()
                                             //join Veh in db.Vehicles on VM.VehicleID equals Veh.VehicleID
                                         join VehM in db.VehicleMake.AsNoTracking() on Veh.MakeID equals VehM.MakeID
                                         join C in db.Color.AsNoTracking() on Veh.VehicleColorID equals C.ColorID
                                         join VehSt in db.StateProvince.AsNoTracking() on Veh.VehicleStateID equals VehSt.StateID
                                         join VehT in db.VehicleType on Veh.VehicleTypeID equals VehT.VehicleTypeID
                                         //join VT in db.VisitorTypes on VM.VisitorTypeID equals VT.VisitorTypeID
                                         where Veh.VehicleID == VehicleID
                                         //orderby Veh.LicensePlateNo descending
                                         select new CheckInVM
                                         {
                                             VehicleID = Veh.VehicleID,
                                             VehicleStateID = VehSt.StateID,
                                             StateName = VehSt.StateName,
                                             VehicleColorID = C.ColorID,
                                             ColorName = C.ColorName,
                                             VehicleTypeID = VehT.VehicleTypeID,
                                             VehicleTypeName = VehT.VehicleTypeName,
                                             ModelName = Veh.ModelName,
                                             MakeID = VehM.MakeID,
                                             MakeName = VehM.MakeName
                                         }
                ).ToList();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult fnGetSearchedVisitorsList(string VisitorFirstName, string VisitorLastName, string Company, string VisitorSSN)
        {
            var resultSet = fnGetFrequesntVisitorsList(VisitorFirstName, VisitorLastName, Company, VisitorSSN);
            if(resultSet!=null)
            {
                return View("CheckIn/_FrequentAndSuggestedVisitersPopulatedView", resultSet);
            }
            else
            {
                return View(resultSet);
            }
        }
        public IEnumerable<FrequentVisitorsList> fnGetFrequesntVisitorsList(string VisitorFirstName, string VisitorLastName, string Company,string VisitorSSN)
        {
            string query = " Select Distinct(VD.[VisitorID]), V.* , D.*,VS.VisitorStatusDescription" +
                            " from Visitor V  " +
                            " JOIN VisitorStatus VS WITH(NOLOCK) ON V.VisitorStatusID=VS.VisitorStatusID "+
                            " Join VisitDetail VD WITH(NOLOCK) on VD.VisitorID = V.VisitorID " +
                            " Join Destination D WITH(NOLOCK) on VD.DestinationID = D.DestinationID " +
                            " Where V.SiteID = " + 1;

            if (VisitorFirstName != "null" && VisitorFirstName != "")
            {
                query = query + " And V.VisitorFirstName LIKE '%" + VisitorFirstName + "%'";
            }
            if (VisitorLastName != "" && VisitorLastName != "null")
            {
                query = query + " And V.VisitorLastName LIKE '%" + VisitorLastName + "%'";
            }

            if (Company != "" && Company != "null")
            {
                query = query + " And V.Company LIKE '%" + Company + "%'";
            }

            if (VisitorSSN != "" && VisitorSSN != "null")
            {
                query = query + " And V.VisitorSSN = '" + VisitorSSN + "'";
            }

            var result=db.Database.SqlQuery<FrequentVisitorsList>(query);
            //if(VisitorFirstName=="")
            //{
            //    VisitorFirstName = "";
            //}
            //if (VisitorLastName == "")
            //{
            //    VisitorLastName = "";
            //}
            //if (VisitorSSN == "")
            //{
            //    VisitorSSN = "";
            //}
            //if (Company == "")
            //{
            //    Company = "";
            //}
            //var result = db.Database
            //.SqlQuery<FrequentVisitorsList>("VMS_GetFrequentVisitsList @VisitorFirstName, @VisitorLastName,@Company,@VisitorSSN",
            //new SqlParameter("@VisitorFirstName", VisitorFirstName),
            //new SqlParameter("@VisitorLastName", VisitorLastName),
            //new SqlParameter("@Company", Company),
            //new SqlParameter("@VisitorSSN", VisitorSSN)
            //)
            //.ToList();
            return result;
        }

        public IEnumerable<VMS_GetAllUsers_Result> getUsersList(int? UserID, int? CompanyID)
        {
            var result = db.Database
            .SqlQuery<VMS_GetAllUsers_Result>("VMS_GetAllUsers @UserID, @CompanyID",
            new SqlParameter("@UserID", UserID),
            new SqlParameter("@CompanyID", CompanyID)
            )
            .ToList();
            return result;
        }

        [HttpPost]
        public JsonResult GetVehiclesLicensePlateNo(string Prefix)
        {
            var vehicleLicensePlateNo = (from c in db.Vehicle
                             where c.LicensePlateNo.Contains(Prefix)
                             select new { c.LicensePlateNo, c.VehicleID });
            return Json(vehicleLicensePlateNo, JsonRequestBehavior.AllowGet);
        }
        #region Usama Baigs work

        public JsonResult fnGetVisitorDetail(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Visitor visitor = db.Visitor.Find(id);
            //Session["VisitorDetail"] = visitor;

            return Json(new { success = true , visitor }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}