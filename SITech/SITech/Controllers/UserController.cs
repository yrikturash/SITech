using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SITech.App_Start;
using SITech.Models;
using SITech.DTO;

namespace SITech.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
       private ApplicationUserManager _userManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        //
        // GET: /Users/All
        public ActionResult Users()
        {
            using (var db = new ApplicationDbContext())
            {
                List<ApplicationUser> users = db.Users.Where(c => c.IsActive).ToList();
                var usersWrap = new List<UserWrapper>();
                foreach (ApplicationUser user in users)
                {
                    IList<string> rolesForUser = UserManager.GetRoles(user.Id);
                    usersWrap.AddRange(from role in rolesForUser
                                        where UserManager.IsInRole(user.Id, "User")
                                        select new UserWrapper
                                        {
                                            UserId = user.Id,
                                            UserName = user.UserName,
                                            MiddleName = user.MiddleName,
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            Email = user.Email,
                                            RoleName = role,
                                            IsActive = user.IsActive
                                        });
                }

                ViewBag.Error = "List is Empty!";

                return View(usersWrap);
            }
        }

        //
        // GET: /User/Add     
        public ActionResult AddUser()
        {
            return View();
        }

        //
        // POST: /User/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MiddleName == null)
                {
                    model.MiddleName = "    ";
                }

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    IsActive = true
                };

                IdentityResult result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    IdentityResult roleAdd = UserManager.AddToRole(user.Id, "User");

                    return RedirectToAction("Users", "User");
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /User/Delete  
        public async Task<ActionResult> DeleteUser(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    ApplicationUser user = await UserManager.FindByIdAsync(id);
                    IList<string> rolesForUser = await UserManager.GetRolesAsync(id);

                    if (rolesForUser.Any())
                    {
                        foreach (string item in rolesForUser)
                        {
                            await UserManager.RemoveFromRoleAsync(user.Id, item);
                        }
                    }

                    await UserManager.DeleteAsync(user);
                }
                return RedirectToAction("Users", "User");
            }
        }

        // GET: /User/Edit 
        public ActionResult EditUser(string userName, AccountController.ManageMessageId? message = null)
        {
            using (var db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.First(u => u.UserName == userName);
                var model = new EditUserViewModel();

                model.UserId = user.Id;
                model.UserName = user.UserName;
                model.FirstName = user.FirstName;
                model.MiddleName = user.MiddleName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.IsActive = user.IsActive;

                ViewBag.MessageId = message;

                return View(model);
            }
        }


        // POST: /User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(model.UserId);

                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.IsActive = model.IsActive;

                IdentityResult result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    if (model.NewPassword != null)
                    {
                        UserManager.RemovePassword(user.Id);
                        UserManager.AddPassword(user.Id, model.NewPassword);
                    }
                    return RedirectToAction("Users", "User");
                }
                AddErrors(result);
            }
            return View(model);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity =
                await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent}, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
	}
}