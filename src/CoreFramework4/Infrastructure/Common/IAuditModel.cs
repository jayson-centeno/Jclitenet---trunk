using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreFramework4.Infrastructure.Common
{
    public interface IAuditModel
    {
        Guid? CreatedBy { get; set; }
        bool? IsDeleted { get; set; }
        DateTime? DateCreated { get; set; }
    }
}
