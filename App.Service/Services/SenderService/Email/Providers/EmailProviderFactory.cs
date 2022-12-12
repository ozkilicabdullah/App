using App.Core.Models;

namespace App.Service.Services.SenderService.Email.Providers
{
    public class EmailProviderFactory
    {
        public IEmailProvider GetProvider(EmailSendingProtokol sendingProtokol)
        {
            IEmailProvider provider = null;

            switch (sendingProtokol)
            {
                case EmailSendingProtokol.SMTP: provider = new SMTPEmailProvider(); break;

                default: break;
            }

            return provider;
        }
    }
}
