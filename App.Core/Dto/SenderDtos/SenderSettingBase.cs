using App.Core.Models;

namespace App.Core.Dto.SenderDtos
{
    public class SenderSettingBase<T>
    {
        public SendingType SendingType { get; set; }
        public T Settings { get; set; }
    }
}
