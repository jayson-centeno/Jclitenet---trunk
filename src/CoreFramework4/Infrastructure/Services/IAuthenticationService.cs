using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFramework4.Implementations.Security;

namespace CoreFramework4.Infrastructure.Services
{
    public interface IAuthenticationService : IService
    {
        bool LogIn(string userName, string password, bool rememberMe);
        void AutoLogin();
        void MakeGuesLogin();
        LogonInfo CurrentUser { get; }
    }
}
