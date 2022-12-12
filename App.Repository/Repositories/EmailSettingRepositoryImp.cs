using App.Core.Models;
using App.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Repositories
{
    public class EmailSettingRepositoryImp : GenericRepositoryImp<EmailSetting>, IEmailSettingRepository
    {
        public EmailSettingRepositoryImp(AppDbContext context) : base(context)
        {
        }

        public async Task<EmailSetting> GetByContentType(SendingContentType sendingContentType)
        {
            var query = (from e in _context.EmailSettings
                         join et in _context.EmailTemplates on e.Id equals et.EmailSettingID
                         where e.Status == Status.Active && et.SendingContentType == sendingContentType
                         select e);
            return await query.FirstOrDefaultAsync();
        }
    }
}
