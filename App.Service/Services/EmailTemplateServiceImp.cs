using App.Core.Dto.SenderDtos;
using App.Core.Models;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;

namespace App.Service.Services
{
    public class EmailTemplateServiceImp : GenericServiceImp<EmailTemplate>, IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        public EmailTemplateServiceImp(IGenericRepository<EmailTemplate> repository, IUnitOfWork unitOfWork, IEmailTemplateRepository emailTemplateRepository) : base(repository, unitOfWork)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public async Task<SenderTemplateDto> GetTemplateBySendingContentType(SendingContentType sendingContentType)
        {
            return await _emailTemplateRepository.GetTemplateBySendingContentType(sendingContentType);
        }
    }
}
