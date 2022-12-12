using App.Core.Dto;
using App.Core.Models;
namespace App.Core.Services
{
    public interface ISenderService<TSenderSetting, TSendingTemplateType>
    {
        Task<CustomResponseDto<TSenderSetting>> GetSenderSettings(SendingContentType sendingContentType);
        Task<CustomResponseDto<NoContentDto>> Send(TSendingTemplateType sendingTemplateType);
    }
}
