using App.Core.Dto.SenderDtos;
using App.Core.Models;

namespace App.Core.Dto
{
    public class EmailSendRequestDto : TemplateBaseDto
    {
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string ReplyAddress { get; set; }
        public string Subject { get; set; }


    }
}
