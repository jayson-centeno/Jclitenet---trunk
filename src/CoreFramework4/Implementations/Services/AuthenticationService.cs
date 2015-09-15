using System;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Implementations.Security;
using System.Web.Security;
using CoreFramework4.Utility;
using System.Web;
using CoreFramework4;

namespace CoreFramework4.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool LogIn(string userName, string password, bool rememberMe)
        {
            var user = _userRepository.GetByEmail(userName);
            if (user != null)
            {
                string truePassword = Cryptography.Decrypt(user.Password, AppConstants.SECRET_CODE);
                if (truePassword == password) {

                    FormsAuthentication.SetAuthCookie(userName, rememberMe);

                    var logoninfo = new LogonInfo()
                    {
                        DateLogin = DateTime.Now,
                        Email = user.Email,
                        Id = user.UID,
                        FullName = user.Name
                    };

                    var principal = new SitePrincipal(logoninfo);
                    HttpContext.Current.User = principal;
                    return true;
                }
            }
            return false;
        }

        public void AutoLogin()
        {
            var cookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var email = ticket.Name;
                if (!String.IsNullOrEmpty(email))
                {
                    var user = _userRepository.GetByEmail(email);
                    if (user != null)
                    {
                        var logoninfo = new LogonInfo()
                        {
                            DateLogin = DateTime.Now,
                            Email = user.Email,
                            Id = user.UID,
                            FullName = user.Name,
                            IsGuest = false
                        };

                        var principal = new SitePrincipal(logoninfo);
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        MakeGuesLogin();
                    }
                }
                else
                {
                    MakeGuesLogin();
                }
            }
            else
            {
                MakeGuesLogin();
            }
        }

        public void MakeGuesLogin()
        {
            var guest = _userRepository.GetByGuidID(new Guid(AppConstants.GUEST_GUID));
            if (guest != null)
            {
                var logoninfo = new LogonInfo()
                {
                    DateLogin = DateTime.Now,
                    Email = guest.Email,
                    Id = guest.UID,
                    FullName = guest.Name,
                    IsGuest = true
                };

                var principal = new SitePrincipal(logoninfo);
                HttpContext.Current.User = principal;
            }
        }

        public LogonInfo CurrentUser
        {
            get { return HttpContext.Current.User.Identity as LogonInfo; }
        }
    }
}
