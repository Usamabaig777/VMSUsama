using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;
using VMS_ASP_NET.Models.Common;

namespace VMS_ASP_NET.Controllers
{
    public class UsersController : Controller
    {
        VMSFarhanEntities db = new VMSFarhanEntities();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                UserRole userRole = (UserRole)Session["userRole"];
                if (userRole.Role.RoleName == "SuperUser" || userRole.Role.RoleName == "Administrator")
                {
                    int siteID = (int)Session["SiteId"];

                    List<UserRole> userList = db.UserRole.Where(m => m.User.SiteID == siteID).ToList();
                    ViewBag.ListofUsers = userList;

                    return View();
                }
                //var sl = new SortedList<string, object>()
                //{
                //{"CompanyID", 1 },
                //{"UserID", 1 }
                //};
                //var loDataTable = DataRepositoryControl.ExecuteTable("VMS_GetAllUsers", sl);
                //Models.Users ViewModel = new Models.Users();
                //ViewModel.Users_Table = loDataTable;
                //return View(ViewModel);
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }

            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult CreateUser()
        {
            if (Session["User"] != null)
            {
                UserRole userRole = (UserRole)Session["userRole"];
                if (userRole.Role.RoleName == "SuperUser" || userRole.Role.RoleName == "Administrator")
                {
                    List<Site> SitesList = db.Site.ToList();
                    List<SelectListItem> AllSites = new List<SelectListItem>();
                    foreach (var item in SitesList)
                    {
                        AllSites.Add(new SelectListItem { Text = item.SiteName, Value = Convert.ToString(item.SiteID) });
                    }

                    Session["AllSitesList"] = AllSites;

                    List<Role> role = db.Role.ToList();
                    Session["roleList"] = role;

                    return View("~/Views/Users/_CreateUser.cshtml");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public User BaseEntitiesAutomation(User user)
        {
            User userSession = (User)Session["User"];
            if (userSession != null)
            {
                user.CreatedBy = userSession.UserID;
            }
            user.CreatedOn = DateTime.Now;
            user.ModifiedBy = 1;
            user.ModifiedOn = DateTime.Now;
            return user;
        }

        public UserRole BaseEntitiesAutomation(UserRole user)
        {
            User userSession = (User)Session["User"];
            if (userSession != null)
            {
                user.CreatedBy = userSession.UserID;
            }
            user.CreatedOn = DateTime.Now;
            user.ModifiedBy = 1;
            user.ModifiedOn = DateTime.Now;
            return user;
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection userForm, User user)
        {
            if (Session["User"] != null)
            {
                UserRole CurrentuserRole = (UserRole)Session["userRole"];
                if (CurrentuserRole.Role.RoleName == "SuperUser" || CurrentuserRole.Role.RoleName == "Administrator")
                {
                    BaseEntitiesAutomation(user);
                    if (ModelState.IsValid)
                    {
                        var EncPass = EncriptDecript.Encrypt(user.Password, true);

                        //string password = Eramake.eCryptography.Encrypt(user.Password);
                        //User u = db.User.FirstOrDefault();
                        //string dycpassword = Eramake.eCryptography.Decrypt(u.Password);

                        //var DycPassword = EncriptDecript.Decrypt(EncPass,true);

                        user.Password = EncPass;
                        db.User.Add(user);
                        db.SaveChanges();
                        UserRole userRole = new UserRole();
                        userRole.RoleID = Convert.ToInt32(userForm["roleID"]);
                        userRole.UserID = user.UserID;
                        BaseEntitiesAutomation(userRole);
                        db.UserRole.Add(userRole);
                        db.SaveChanges();
                        TempData["sucessMsg"] = "User is saved successfully";
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult CheckDuplicateUser(string userNameField)
        {
            db.Configuration.ProxyCreationEnabled = false;
            User user = db.User.Where(m => m.UserName == userNameField).FirstOrDefault();

            return Json(new { success = true, user }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUser(int id)
        {
            if (Session["User"] != null)
            {
                UserRole CurrentuserRole = (UserRole)Session["userRole"];
                if (CurrentuserRole.Role.RoleName == "SuperUser" || CurrentuserRole.Role.RoleName == "Administrator")
                {
                    User user = db.User.Find(id);
                    Session["GetUser"] = user;
                    return View("~/Views/Users/_EditUser.cshtml");
                }
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (Session["User"] != null)
            {
                UserRole CurrentuserRole = (UserRole)Session["userRole"];
                if (CurrentuserRole.Role.RoleName == "SuperUser" || CurrentuserRole.Role.RoleName == "Administrator")
                {
                    var EncPass = EncriptDecript.Encrypt(user.Password, true);

                    User updateUser = db.User.Find(user.UserID);
                    updateUser.Password = EncPass;
                    updateUser.FirstName = user.FirstName;
                    updateUser.LastName = user.LastName;
                    updateUser.UserName = user.UserName;
                    updateUser.isActive = user.isActive;
                    updateUser.Password = EncPass;

                    User GetUser= (User)Session["User"];
                    user.ModifiedBy = GetUser.UserID;
                    user.ModifiedOn = DateTime.Now;
                    db.SaveChanges();

                    TempData["sucessMsg"] = "User is updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ChangeUserRole(int id)
        {
            if (Session["User"] != null)
            {
                UserRole CurrentuserRole = (UserRole)Session["userRole"];
                if (CurrentuserRole.Role.RoleName == "SuperUser" || CurrentuserRole.Role.RoleName == "Administrator")
                {
                    UserRole user = db.UserRole.Where(m => m.UserID == id).FirstOrDefault();
                    Session["UserRoleName"] = user;

                    List<Role> role = db.Role.ToList();
                    Session["RoleList"] = role;

                    return View("~/Views/Users/_ChangeUserRole.cshtml");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult UpdateUserRole(int roleID, int userRoleID)
        {
            if (Session["User"] != null)
            {
                UserRole CurrentuserRole = (UserRole)Session["userRole"];
                if (CurrentuserRole.Role.RoleName == "SuperUser" || CurrentuserRole.Role.RoleName == "Administrator")
                {
                    UserRole userRole = db.UserRole.Find(userRoleID);
                    userRole.RoleID = roleID;
                    TryUpdateModel(userRole);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}