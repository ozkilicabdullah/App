using App.Core.Services;


namespace App.Service.Services.SenderService
{
    public abstract class BaseTemplateParser : ITemplateParserService
    {
        public string FillTemplate(Dictionary<string, string> ReplacePairs, string TemplateText)
        {
            if (string.IsNullOrWhiteSpace(TemplateText)) return "";
            if (ReplacePairs == null || ReplacePairs.Count <= 0) return TemplateText;

            if (ReplacePairs != null && ReplacePairs.Count > 0)
            {
                foreach (var item in ReplacePairs)
                {
                    TemplateText = TemplateText.Replace(item.Key, item.Value);
                }
            }

            return TemplateText;
        }
    }

    public class EmailTemplateParser : BaseTemplateParser, ITemplateParserService
    {

    }
}
