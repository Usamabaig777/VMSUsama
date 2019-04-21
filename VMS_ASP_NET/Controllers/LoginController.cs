using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;
using VMS_ASP_NET.Models.Common;

namespace VMS_ASP_NET.Controllers
{
    public class LoginController : Controller
    {
        VMSEntities db = new VMSEntities();
        // GET: Login
        
        [HttpPost]
        public ActionResult Index(Login login)
        {
            Session["CurrentController"] = "Login";
            if (ModelState.IsValid)
            {
                var EncPass=EncriptDecript.Encrypt(login.Password, true);
                User user = db.Users.Where(m => m.UserName == login.UserName && m.Password == EncPass).FirstOrDefault();
                if (user != null)
                {
                    Session["LoginFail"] = null;
                    int id = user.UserID;
                    UserRole userRole = db.UserRoles.Where(m => m.UserID == id).FirstOrDefault();
                    Session["userRole"] = userRole;

                    Session["User"] = user;

                    ModelState.Clear();
                    //ViewBag.UserMessage = "Login Successfully";
                    if (userRole.Role.RoleName == "SuperUser")
                    {
                        Site site = db.Sites.FirstOrDefault();
                        Session["SiteId"] = site.SiteID;
                        return RedirectToAction("Index", "Dashboard");

                    }
                    else
                    {
                        return RedirectToAction("SitesGates", "Login");
                    }
                }
                else
                {
                    Session["ErrorMessage"] = "Invalid Username or Password!";
                    Session["LoginFail"] = login;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SitesGates()
        {
            List<Site> siteList = db.Sites.ToList();
            foreach (var item in siteList)
            {
                ViewData["sites"] = new SelectList(siteList, "SiteID", "SiteName");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SitesGates(FormCollection sites)
        {
            int SiteId = Convert.ToInt32(sites["SiteID"]);
            Session["SiteID"] = SiteId;
            Site site = db.Sites.Find(SiteId);
            int CompanyId = site.CompanyID;
            Session["CompanyID"] = CompanyId;
            //int GateId = Convert.ToInt32(sites["GateID"]);
            //Session["GateId"] = GateId;
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult GetGates(int id)
        {

            List<Gate> gateList = db.Gates.Where(m => m.SiteID == id).ToList();
            foreach (var item in gateList)
            {
                ViewData["gates"] = new SelectList(gateList, "GateID", "GateDescription");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["User"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                User user = (User)Session["User"];
                if (user != null)
                {
                    User dbuser = db.Users.Find(user.UserID);
                    if (dbuser.Password == changePassword.OldPassword)
                    {
                        dbuser.Password = changePassword.NewPassword;
                        TryUpdateModel(dbuser);
                        db.SaveChanges();
                        TempData["ChangePassSuccecc"] = "Password is updated successfully!";
                    }
                    else
                    {
                        ViewBag.Error = "Old password is wrong";
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public ActionResult SiteSelectionView(int id)
        {
            Session["SiteId"] = id;
            string controller = (string)Session["CurrentController"];
            return RedirectToAction("Index", controller);
        }

    }
}