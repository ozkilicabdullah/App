using App.Core.Dto;
using App.Core.Dto.SenderDtos.EmailSenderDtos;


namespace App.Service.Services.SenderService.Email.Providers
{
    public interface IEmailProvider
    {
        bool Send(EmailConfigurationDto emailConfiguration, EmailSendRequestDto emailSendRequestDto);
    }
}
