// Type: System.Net.Mail.SmtpClient
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.dll

using System;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Net.Mail
{
    public class SmtpClient : IDisposable
    {
        public SmtpClient();
        public SmtpClient(string host);
        public SmtpClient(string host, int port);
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public ICredentialsByHost Credentials { get; set; }
        public int Timeout { get; set; }
        public ServicePoint ServicePoint { get; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public string PickupDirectoryLocation { get; set; }
        public bool EnableSsl { get; set; }
        public X509CertificateCollection ClientCertificates { get; }
        public string TargetName { get; set; }

        #region IDisposable Members

        public void Dispose();

        #endregion

        protected void OnSendCompleted(AsyncCompletedEventArgs e);
        public void Send(string from, string recipients, string subject, string body);
        public void Send(MailMessage message);

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public void SendAsync(string from, string recipients, string subject, string body, object userToken);

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public void SendAsync(MailMessage message, object userToken);

        public void SendAsyncCancel();
        protected virtual void Dispose(bool disposing);
        public event SendCompletedEventHandler SendCompleted;
    }
}
