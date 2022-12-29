using App.Core.Dto;
using App.Core.Dto.SenderDtos.EmailSenderDtos;
using App.Core.Models;
using App.Core.Services;
using App.Service.Services.SenderService.Email.Providers;

namespace App.Service.Services.SenderService.Email
{
    public class EmailSenderService : ISenderService<EmailSendingSettings, EmailSendRequestDto>
    {
        private readonly EmailProviderFactory emailProviderFactory;
        private IEmailProvider emailProvider;
        private readonly IEmailSettingService _emailSettingService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ITemplateParserService _templateParserService;

        public EmailSenderService(IEmailSettingService emailSettingService, IEmailTemplateService emailTemplateService)
        {
            emailProviderFactory = new EmailProviderFactory();
            _templateParserService = new EmailTemplateParser();
            _emailSettingService = emailSettingService;
            _emailTemplateService = emailTemplateService;
        }


        /// <summary>
        /// Sets Current Email Provider, SMTP etc.
        /// </summary>
        /// <param name="provider"></param>
        private void SetProvider(IEmailProvider provider)
        {
            this.emailProvider = provider;
        }

        public async Task<CustomResponseDto<EmailSendingSettings>> GetSenderSettings(SendingContentType sendingContentType)
        {
            var emailSetting = await _emailSettingService.GetByContentType(sendingContentType);

            if (emailSetting == null)
                return CustomResponseDto<EmailSendingSettings>.Fail(400, "No record found for content type!");

            EmailSendingSettings emailSendingSettings = new()
            {
                Settings = MapToEmailConfiguration(emailSetting)
            };

            return CustomResponseDto<EmailSendingSettings>.Success(200, emailSendingSettings);
        }

        private bool Send(EmailConfigurationDto emailConfiguration, EmailSendRequestDto emailSendRequestDto)
        {
            if (this.emailProvider == null)
                return false;
            return emailProvider.Send(emailConfiguration, emailSendRequestDto);
        }
        public async Task<CustomResponseDto<NoContentDto>> Send(EmailSendRequestDto emailSendRequestDto)
        {
            // get sender setting
            var senderSettings = await GetSenderSettings(emailSendRequestDto.SendingContentType);
            if (!senderSettings.IsSuccess)
                return CustomResponseDto<NoContentDto>.Fail(400, senderSettings.Errors);

            EmailConfigurationDto emailConfigurationDto = senderSettings.Data.Settings;

            // set provider
            var provider = emailProviderFactory.GetProvider(emailConfigurationDto.SendingProtokol);
            this.SetProvider(provider);
            // get template
            emailSendRequestDto.SendingTemplate = await _emailTemplateService.GetTemplateBySendingContentType(emailSendRequestDto.SendingContentType);
            // replace content
            emailSendRequestDto.Message = _templateParserService.FillTemplate(emailSendRequestDto.ReplacePairs, emailSendRequestDto.SendingTemplate.Detail);

            bool result = Send(emailConfigurationDto, emailSendRequestDto);
            if (!result)
                return CustomResponseDto<NoContentDto>.Fail(400, "An error occurred while sending the mail");

            return CustomResponseDto<NoContentDto>.Success(204);
        }
        private EmailConfigurationDto MapToEmailConfiguration(EmailSetting emailSetting)
        {
            if (emailSetting == null) return null;
            var emailConfigurationmappedDto = new EmailConfigurationDto()
            {
                SendingProtokol = emailSetting.SendingProtokol,
                FromName = emailSetting.Name,
                FromEmail = emailSetting.SenderEmail
            };

            switch (emailSetting.SendingProtokol)
            {
                case EmailSendingProtokol.SMTP:
                    emailConfigurationmappedDto.SmtpSettings = new SmtpSetting
                    {
                        PrimaryMailEnableSsl = emailSetting.EnableSsl,
                        PrimaryMailHost = emailSetting.Host,
                        PrimaryMailPassword = emailSetting.Password,
                        PrimaryMailPort = emailSetting.Port,
                        PrimaryMailSenderName = emailSetting.SenderName,
                        PrimaryMailUserName = emailSetting.UserName,
                        SendingProtokol = emailSetting.SendingProtokol,
                        PrimaryMailSenderEmail = emailSetting.SenderEmail
                    };
                    break;
                default:
                    break;
            }
            return emailConfigurationmappedDto;
        }
    }
}

