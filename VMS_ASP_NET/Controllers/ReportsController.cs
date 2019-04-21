using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;

namespace VMS_ASP_NET.Controllers
{
    public class ReportsController : Controller
    {
        VMSEntities db = new VMSEntities();
        // GET: Reports
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                Session["CurrentController"] = "Reports";
                //Get All Incident Types for DropDown
                var incidentReportTypes = db.IncidentTypes.ToList();
                List<SelectListItem> AllIncidents = new List<SelectListItem>();
                foreach (var item in incidentReportTypes)
                {
                    AllIncidents.Add(new SelectListItem { Text = item.IncidentTypeDescription, Value = Convert.ToString(item.IncidentTypeID) });
                }
                Session["AllIncident"] = AllIncidents;
                //Session["AllIncident"] = new SelectList(db.IncidentTypes.ToList(), "IncidentTypeID", "IncidentTypeDescription");

                //Get All Vehicle License State
                List<StateProvince> States = db.StateProvinces.ToList();
                //List<SelectListItem> AllStates = new List<SelectListItem>();
                //foreach (var item in States)
                //{
                //    AllStates.Add(new SelectListItem { Text = item.StateCode + " ( " + item.StateName + " )", Value = Convert.ToString(item.StateID) });
                //}
                Session["AllStaesList"] = States;

                List<VehicleMake> AllMake = db.VehicleMakes.ToList();
                List<SelectListItem> AllVehicleMake = new List<SelectListItem>();
                foreach (var item in AllMake)
                {
                    AllVehicleMake.Add(new SelectListItem { Text = item.MakeName, Value = Convert.ToString(item.MakeID) });
                }
                Session["allMake"] = AllVehicleMake;

                List<TrailerAttachment> trailerAttachment = db.TrailerAttachments.ToList();
                List<SelectListItem> trailer = new List<SelectListItem>();
                foreach (var item in trailerAttachment)
                {
                    trailer.Add(new SelectListItem { Text = item.TrailerAttachmentName, Value = Convert.ToString(item.TrailerAttachmentID) });
                }
                Session["trailerList"] = trailer;

                int siteID = (int)Session["SiteId"];
                List<IncidentReport> incidentReports = db.IncidentReports.Where(m => m.SiteID == siteID).ToList();
                ViewBag.IncidentReports = incidentReports;

                return View();
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

            //var sl = new SortedList<string, object>()
            //{
            //    {"SiteID", 1 }
            //};
            ////get incident report list
            //var loDataTable = DataRepositoryControl.ExecuteTable("VMS_GetAllIncidentReports", sl);
            //Models.Reports ViewModel = new Models.Reports();
            //ViewModel.Incident_Reports= loDataTable;
            //return View(ViewModel);

        }

        public ActionResult CreateIncidentReport()
        {
            return View();
        }

        public IncidentReport BaseEntitiesAutomation(IncidentReport incident)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                incident.UserID = user.UserID;
                incident.ModifiedBy = user.UserID;
                incident.CreatedBy = user.UserID;
            }

            int siteID = (int)Session["SiteId"];
            if (siteID != 0)
            {
                incident.SiteID = siteID;
            }
            incident.CreatedOn = DateTime.Now;
            //incident.IncidentDateTime = DateTime.Now;
            incident.ModifiedOn = DateTime.Now;
            return incident;
        }
        public IncidentReportType BaseEntitiesAutomation(IncidentReportType incident)
        {
            incident.CreatedBy = 1;
            User user = (User)Session["User"];
            if (user != null)
            {
                incident.ModifiedBy = user.UserID;
                incident.CreatedBy = user.UserID;
            }
            incident.CreatedOn = DateTime.Now;
            incident.ModifiedOn = DateTime.Now;
            return incident;
        }

        [HttpPost]
        public ActionResult CreateIncidentReport(FormCollection GetReport, IncidentReport incidentReport,string FileName)
        {
            //= Request['files']
            incidentReport.IncidentDateTime = Convert.ToDateTime(GetReport["IncidentDate"] +" "+ GetReport["IncidentTime"]);
            BaseEntitiesAutomation(incidentReport);
            db.IncidentReports.Add(incidentReport);
            db.SaveChanges();
   
            IncidentReportAttachment incidentReportAttachment = new IncidentReportAttachment();
            

            //IncidentReportType incidentReportType = new IncidentReportType();
            //string[] IncValues = GetReport.GetValues("Incident");

            //foreach (var item in IncValues)
            //{
            //    int id = Convert.ToInt32(item);
            //    incidentReportType.IncidentTypeID = id;
            //    incidentReportType.IncidentReportID = incidentReport.IncidentReportID;
            //    BaseEntitiesAutomation(incidentReportType);
            //    db.IncidentReportTypes.Add(incidentReportType);
            //    db.SaveChanges();
            //}
            return RedirectToAction("Index", "Reports");
        }

        public ActionResult UpdateIncidentReport(int id)
        {
            IncidentReport incReport = db.IncidentReports.Find(id);
            ViewBag.incidentReport = incReport;

            return View();
        }
        [HttpPost]
        public ActionResult UpdateIncidentReport(FormCollection GetReport, IncidentReport incidentReport)
        {
            IncidentReport inc = db.IncidentReports.Find(incidentReport.IncidentReportID);
            TryUpdateModel(inc);
            db.SaveChanges();
            return RedirectToAction("Index", "Reports");
        }

        public ActionResult AddAttachment()
        {
            return View("~/Views/Reports/AddAttachment.cshtml");
        }

        public ActionResult OperationalReport()
        {
            return View();
        }

    }
}