using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;

namespace VMS_ASP_NET.Controllers
{
    public class VisitorsController : Controller
    {
        private VMSFarhanEntities db = new VMSFarhanEntities();
        
        // GET: Visitors
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                var visitors = db.Visitor.Include(v => v.Site).Include(v => v.VisitorStatu);
                return View(visitors.ToList());
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

            
        }

        // GET: Visitors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitor.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // GET: Visitors/Create
        public ActionResult Create()
        {
            ViewBag.SiteID = new SelectList(db.Site.AsNoTracking(), "SiteID", "SiteName");
            ViewBag.VisitorStatusID = new SelectList(db.VisitorStatu.AsNoTracking(), "VisitorStatusID", "VisitorStatusDescription");
            return View();
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VisitorID,SiteID,VisitorSSN,VisitorFirstName,VisitorLastName,VisitorAddress,VisitorEmail,Company,PassNo,VisitorStatusID,VisitorEntryExitNotification,NotificationEmailAddress,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                db.Visitor.Add(visitor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SiteID = new SelectList(db.Site.AsNoTracking(), "SiteID", "SiteName", visitor.SiteID);
            ViewBag.VisitorStatusID = new SelectList(db.VisitorStatu.AsNoTracking(), "VisitorStatusID", "VisitorStatusDescription", visitor.VisitorStatusID);
            return View(visitor);
        }

        // GET: Visitors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitor.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteID = new SelectList(db.Site.AsNoTracking(), "SiteID", "SiteName", visitor.SiteID);
            ViewBag.VisitorStatusID = new SelectList(db.VisitorStatu.AsNoTracking(), "VisitorStatusID", "VisitorStatusDescription", visitor.VisitorStatusID);
            return View(visitor);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VisitorID,SiteID,VisitorSSN,VisitorFirstName,VisitorLastName,VisitorAddress,VisitorEmail,Company,PassNo,VisitorStatusID,VisitorEntryExitNotification,NotificationEmailAddress,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SiteID = new SelectList(db.Site.AsNoTracking(), "SiteID", "SiteName", visitor.SiteID);
            ViewBag.VisitorStatusID = new SelectList(db.VisitorStatu.AsNoTracking(), "VisitorStatusID", "VisitorStatusDescription", visitor.VisitorStatusID);
            return View(visitor);
        }

        // GET: Visitors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitor.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitor visitor = db.Visitor.Find(id);
            db.Visitor.Remove(visitor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
