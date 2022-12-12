

using App.Core.Models;

namespace App.Core.Dto.SenderDtos.EmailSenderDtos
{
    public class SmtpSetting
    {
        public string PrimaryMailSenderName { get; set; }
        public string PrimaryMailSenderEmail { get; set; }
        public string PrimaryMailReplyEmail { get; set; }
        public string PrimaryMailReplyName { get; set; }

        public string PrimaryMailUserName { get; set; }
        public string PrimaryMailPassword { get; set; }
        public int PrimaryMailPort { get; set; }
        public string PrimaryMailHost { get; set; }
        public bool PrimaryMailEnableSsl { get; set; }
        public EmailSendingProtokol SendingProtokol { get; set; }
    }
}
