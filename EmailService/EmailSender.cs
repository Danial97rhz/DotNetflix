using System.Net;
using System.Net.Mail;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MailMessage CreateEmailMessage(Message message)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(message.To);
            mailMessage.From = new MailAddress(_emailConfig.From);
            mailMessage.Subject = message.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message.Content;

            return mailMessage;
        }

        private void Send(MailMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Host = _emailConfig.SmtpServer;
                    client.Port = _emailConfig.Port;
                    client.Credentials = new NetworkCredential(_emailConfig.UserName, _emailConfig.Password);
                    client.EnableSsl = true;

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}
