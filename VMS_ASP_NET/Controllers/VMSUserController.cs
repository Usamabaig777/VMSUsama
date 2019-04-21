using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS_ASP_NET.Models;
using VMS_ASP_NET.Models.ViewModels;

namespace VMS_ASP_NET.Controllers
{
    public class VMSUserController : Controller
    {
        
        // GET: VMSUser
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                VMSFarhanEntities db = new VMSFarhanEntities();
                var data = (from usr in db.User.AsNoTracking()
                            join usrRole in db.UserRole.AsNoTracking() on usr.UserID equals usrRole.UserID
                            join role in db.Role.AsNoTracking() on usrRole.RoleID equals role.RoleID
                            select new UserRolesViewModel
                            {
                                UserName = usr.UserName,
                                FirstName = usr.FirstName,
                                LastName = usr.LastName,
                                RoleName = role.RoleName,
                                isExternal = usr.isExternal,
                                isActive = usr.isActive,
                                isLogin = usr.isLogin,
                                isPasswordReset = usr.isPasswordReset
                            }).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public UserRolesViewModel CreateUserViewMode()
        {
            var viewModel = new UserRolesViewModel();

            return viewModel;
        }
    }
}