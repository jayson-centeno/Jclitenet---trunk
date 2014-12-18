using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreFramework4.Infrastructure.Security
{
    public interface ILogonInfo
    {
        Guid Id { get; set; }
        DateTime? DateLogin { get; set; }
        string Email { get; set; }
        string FullName { get; set; }
        bool IsGuest { get; set; }
    }
}
