using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework4.Infrastructure.Services
{
    public interface IMailService : IService
    {
        void Send(string body, string subject, string fromAddress, string fromDisplayName, string toAddress, string toDisplayName);
        void SendNewComment(string commentorName, string comment, string topicTitle);
        void SendContactMe(string email, string message);
        void SendDefault(string body, string subject);
    }
}
