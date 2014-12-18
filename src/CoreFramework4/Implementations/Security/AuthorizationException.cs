using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CoreFramework4.Implementations.Security
{
    public class AuthorizationException : HttpException
    {
        public override string Message
        {
            get
            {
                return "UnAuthorized Access!";
            }
        }

        public override int ErrorCode 
        {
            get
            {
                return 401;
            }
        }
    }
}
