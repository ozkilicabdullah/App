using App.Core.Models;

namespace App.Core.Dto
{
    public class EmailTemplateDto : BaseDtoModel
    {
        public string Name { get; set; }
        public SendingContentType SendingContentType { get; set; }
        public int EmailSettingID { get; set; }
        public string DescriptionHTML { get; set; }
        public string DescriptionText { get; set; }
    }
}
