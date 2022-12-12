using App.Core.Dto.SenderDtos;
using App.Core.Models;
using App.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Repositories
{
    public class EmailTemplateRepositoryImp : GenericRepositoryImp<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepositoryImp(AppDbContext context) : base(context)
        {
        }

        public async Task<SenderTemplateDto> GetTemplateBySendingContentType(SendingContentType sendingContentType)
        {
            var query = (from et in _context.EmailTemplates
                         where et.Status == Status.Active && et.SendingContentType == sendingContentType
                         select new SenderTemplateDto
                         {
                             Detail = et.DescriptionHTML,
                             DetailText = et.DescriptionText,
                             Id = et.Id,
                             Name = et.Name
                         });
            return await query.FirstOrDefaultAsync();
        }
    }
}
