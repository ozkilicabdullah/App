using App.Core.Dto;
using App.Core.Dto.SenderDtos.EmailSenderDtos;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace App.Service.Services.SenderService.Email.Providers
{
    public class SMTPEmailProvider : IEmailProvider
    {
        bool IEmailProvider.Send(EmailConfigurationDto emailConfiguration, EmailSendRequestDto emailSendRequestDto)
        {
            if (emailConfiguration == null || emailSendRequestDto == null) return false;

            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpSetting smtpSet = emailConfiguration.SmtpSettings;
                mailMessage.To.Add(new MailAddress(emailSendRequestDto.ToEmail, emailSendRequestDto.ToName));

                string SenderEmail = (string.IsNullOrWhiteSpace(smtpSet.PrimaryMailSenderEmail)) ? smtpSet.PrimaryMailUserName : smtpSet.PrimaryMailSenderEmail;
                string SenderName = (string.IsNullOrWhiteSpace(smtpSet.PrimaryMailSenderName)) ? "" : smtpSet.PrimaryMailSenderName;
                string ReplyName = (string.IsNullOrWhiteSpace(smtpSet.PrimaryMailReplyName)) ? smtpSet.PrimaryMailSenderName : smtpSet.PrimaryMailReplyName;

                mailMessage.From = new MailAddress(SenderEmail, SenderName, Encoding.UTF8);

                mailMessage.Subject = emailSendRequestDto.Subject;
                mailMessage.Body = emailSendRequestDto.Message;
                mailMessage.IsBodyHtml = true;
                NetworkCredential myCredentialDB = new NetworkCredential(emailConfiguration.SmtpSettings.PrimaryMailUserName, emailConfiguration.SmtpSettings.PrimaryMailPassword);

                SmtpClient smtpClientDB = new SmtpClient
                {
                    Host = emailConfiguration.SmtpSettings.PrimaryMailHost,
                    UseDefaultCredentials = false,
                    Credentials = myCredentialDB,
                    EnableSsl = emailConfiguration.SmtpSettings.PrimaryMailEnableSsl,
                    Port = emailConfiguration.SmtpSettings.PrimaryMailPort
                };
                smtpClientDB.ServicePoint.MaxIdleTime = 1;
                smtpClientDB.Send(mailMessage);

            }
            catch (Exception ex)
            {
                // loggin operation
                return false;
            }

            return true;
        }
    }
}
