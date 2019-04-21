using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;

namespace VMS_ASP_NET.Controllers
{
    public class SitesController : Controller
    {
        VMSFarhanEntities db = new VMSFarhanEntities();
        // GET: Sites
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                Session["CurrentController"] = "Sites";
                var sl = new SortedList<string, object>()
            {
                {"CompanyID", 1 }
            };
                var loDataTable = DataRepositoryControl.ExecuteTable("usp_GetAllSites", sl);
                Models.Sites ViewModel = new Models.Sites();
                ViewModel.Sites_Tables = loDataTable;
                return View(ViewModel);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult CreateSite()
        {
            return View();
        }

        public Site BaseEntitiesAutomation(Site sites)
        {
            sites.CreatedBy = 1;
            int siteID = (int)Session["SiteId"];
            if (siteID != 0)
            {
                sites.SiteID = siteID;
            }
            sites.CreatedOn = DateTime.Now;
            sites.ModifiedBy = 1;
            sites.CompanyID = 1;
            sites.ModifiedOn = DateTime.Now;
            return sites;
        }

        [HttpPost]
        public ActionResult CreateSite(FormCollection getSite , Site site)
        {
            BaseEntitiesAutomation(site);
            if (ModelState.IsValid)
            {
                db.Site.Add(site);
                db.SaveChanges();
            }

            return RedirectToAction("Index","Sites");
        }

        public ActionResult UpdateSite(int id)
        {
            Site site = db.Site.Find(id);
            ViewBag.Sites = site;

            return View();
        }

        [HttpPost]
        public ActionResult UpdateSite(FormCollection updateSite,Site site)
        {
            Site st = db.Site.Find(site.SiteID);
            TryUpdateModel(st);
            db.SaveChanges();
            return RedirectToAction("Index", "Sites");
        }

        public ActionResult getGatesBySiteID(int SiteID)
        {
            var gate = db.Gate.Where(x => x.SiteID == SiteID).ToList().OrderBy(x => x.GateDescription);
            return View("~/Views/Sites/_GetGates.cshtml" , gate);
        }

        public ActionResult UpdateGate(int id)
        {
            Gate gate = db.Gate.Find(id);
            return View("~/Views/Sites/_EditGates.cshtml", gate);
        }

        [HttpPost]
        public ActionResult UpdateGate(FormCollection updateGate, Gate gate)
        {
            Gate gt = db.Gate.Find(gate.GateID);
            TryUpdateModel(gt);
            db.SaveChanges();
            return RedirectToAction("Index", "Sites");
        }

        public Gate BaseEntitiesAutomation(Gate gate)
        {
            gate.CreatedBy = 1;
            //int siteID = (int)Session["SiteId"];
            //if (siteID != 0)
            //{
            //    gate.SiteID = siteID;
            //}
            gate.CreatedOn = DateTime.Now;
            gate.ModifiedBy = 1;
            gate.ModifiedOn = DateTime.Now;
            return gate;
        }

        //public ActionResult CreateGate(int id)
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult CreateGate(FormCollection AddGate, Gate gate)
        {
            BaseEntitiesAutomation(gate);
            if (ModelState.IsValid)
            {
                db.Gate.Add(gate);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Sites");
        }

    }
}