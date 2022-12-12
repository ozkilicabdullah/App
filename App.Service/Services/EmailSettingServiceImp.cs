using App.Core.Models;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;

namespace App.Service.Services
{
    public class EmailSettingServiceImp : GenericServiceImp<EmailSetting>, IEmailSettingService
    {
        private readonly IEmailSettingRepository _emailSettingRepository;
        public EmailSettingServiceImp(IGenericRepository<EmailSetting> repository, IUnitOfWork unitOfWork, IEmailSettingRepository emailSettingRepository) : base(repository, unitOfWork)
        {
            _emailSettingRepository = emailSettingRepository;
        }

        public async Task<EmailSetting> GetByContentType(SendingContentType sendingContentType)
        {
            return await _emailSettingRepository.GetByContentType(sendingContentType);
        }
    }
}
