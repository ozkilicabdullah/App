using App.Core.Models;

namespace App.Core.Dto.SenderDtos
{
    public abstract class TemplateBaseDto
    {
        public TemplateBaseDto()
        {
            this.ReplacePairs = new Dictionary<string, string>();
        }

        public void AddReplacePair(string key, string value)
        {
            ReplacePairs.Add(key, value);
        }

        public void AddReplacePairForMessage(string key)
        {
            ReplacePairs.Add(key, this.Message);
        }
        public Dictionary<string, string> ReplacePairs { get; set; }
        public string Message { get; set; }
        public SenderTemplateDto SendingTemplate { get; set; }
        public SendingContentType SendingContentType { get; set; }


    }
}
