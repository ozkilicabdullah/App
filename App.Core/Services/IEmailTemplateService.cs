using App.Core.Dto.SenderDtos;
using App.Core.Models;

namespace App.Core.Services
{
    public interface IEmailTemplateService : IGenericService<EmailTemplate>
    {
        Task<SenderTemplateDto> GetTemplateBySendingContentType(SendingContentType sendingContentType);

    }
}
