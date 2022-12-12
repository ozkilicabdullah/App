using App.Core.Dto.SenderDtos;
using App.Core.Models;


namespace App.Core.Repositories
{
    public interface IEmailTemplateRepository : IGenericRepository<EmailTemplate>
    {
        Task<SenderTemplateDto> GetTemplateBySendingContentType(SendingContentType sendingContentType);
    }
}
