namespace App.Core.Dto.SenderDtos.EmailSenderDtos
{
    public class EmailSendingSettings: SenderSettingBase<EmailConfigurationDto>
    {
        public EmailSendingSettings()
        {
            this.SendingType=Models.SendingType.Email;
        }
    }
}
