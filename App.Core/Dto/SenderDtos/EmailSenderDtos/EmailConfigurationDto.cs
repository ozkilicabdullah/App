

using App.Core.Models;

namespace App.Core.Dto.SenderDtos.EmailSenderDtos
{
    public class EmailConfigurationDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string FromName { get; set; }
        public string FromEmail { get; set; }
        private SmtpSetting smtpSetting { get; set; }
        public SmtpSetting SmtpSettings
        {
            get
            {
                return smtpSetting;
            }
            set
            {

                FromName = (value != null && value.PrimaryMailSenderName != null) ? value.PrimaryMailSenderName : FromName;
                smtpSetting = value;
            }
        }
        public EmailSendingProtokol SendingProtokol { get; set; }

    }
}
