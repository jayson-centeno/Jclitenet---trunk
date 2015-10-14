using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jclitenet.Domain.Objects.Poco
{
    public class ResumeDetails
    {
        public Guid ID { get; set; }

        public Guid ResumeID { get; set; }

        public string Introduction { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string HomePhone { get; set; }

        public string Skills { get; set; }

        public string Interests { get; set; }
    }
}
