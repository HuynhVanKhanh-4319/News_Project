using Web_ProjectName.Common.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using Web_ProjectName.Models;

namespace Web_ProjectName.Lib
{
    public interface ISendMailSMTP
    {
        Task<int> SendMail(string toMail, string subject, string message);
        Task<int> SendMailBySupplierConfig(M_SupplierConfig supplierConfig, string toMail, string subject, string message);
    }
    public class SendMailSMTP : ISendMailSMTP
    {
        private readonly IOptions<Config_MailSMPTConfig> _configuration;

        public SendMailSMTP(IOptions<Config_MailSMPTConfig> configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SendMail(string toMail, string subject, string message)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_configuration.Value.host);

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(_configuration.Value.fromEmail, _configuration.Value.password);
                //smtpClient.Credentials = new System.Net.NetworkCredential("lenhatnamit07@gmail.com", "bzlrxhtcqumqtwef");
                // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Port = 587;
                MailMessage mail = new MailMessage();
                //Setting From , To and CC
                mail.From = new MailAddress(_configuration.Value.fromEmail, _configuration.Value.displayName);
                mail.To.Add(new MailAddress(toMail ?? _configuration.Value.toEmail));
                //mail.CC.Add(new MailAddress("nhanit2012.vn@gmail.com"));
                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = message;
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }
        public async Task<int> SendMailBySupplierConfig(M_SupplierConfig supplierConfig, string toMail, string subject, string message)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(supplierConfig.mailHost);

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(supplierConfig.mailFrom, Encryptor.Decrypt(supplierConfig.mailPassword));
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Port = 587;
                MailMessage mail = new MailMessage();
                //Setting From , To and CC
                mail.From = new MailAddress(supplierConfig.mailFrom, supplierConfig.mailDisplayName);
                mail.To.Add(new MailAddress(toMail));
                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = message;
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }
    }
}
