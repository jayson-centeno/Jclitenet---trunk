using System;
using System.Web.Mvc;
using System.Web.Security;
using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Model;
using jclitenet.Models;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Implementations.Security;

namespace jclitenet.Controllers
{
    [HandleError]
    public class AccountController : SiteBaseController
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var authenticationService = ServiceFactory.GetInstance<IAuthenticationService>();
                bool login = authenticationService.LogIn(model.UserName, model.Password, model.RememberMe);
                if (!login)
                {
                    ModelState.AddModelError("", "Username or Password is invalid.");
                    Response.Redirect("/");
                }
                else
                    return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            return View();
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                var user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                //userRepository.GetByGuidID(user.ProviderUserKey
                var roles = Roles.GetAllRoles();
                //Roles.AddUsersToRole(model.UserName, "");

                if (createStatus == MembershipCreateStatus.Success)
                {
                    var userRepository = ServiceFactory.GetInstance<IUserRepository>();
                    var userInfo = new User()
                    {
                        User_PKID = (Guid)user.ProviderUserKey,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        IsGuest = false,
                        Email = "",
                        UID = Guid.NewGuid()
                    };

                    try
                    {
                        userRepository.Add(userInfo);
                        userRepository.SaveChanges();
                    }
                    catch (Exception)
                    {
                        Membership.DeleteUser(user.UserName);
                        throw;
                    }

                    FormsAuthentication.SetAuthCookie(userInfo.User_PKID.ToString(), false /* createPersistentCookie */);
                    //HttpContext.User = new SitePrincipal(userInfo);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [SiteAuthorization]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [SiteAuthorization]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                    return RedirectToAction("ChangePasswordSuccess");

                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
