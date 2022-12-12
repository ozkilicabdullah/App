
namespace App.Core.Services
{
    public interface ITemplateParserService
    {
        string FillTemplate(Dictionary<string, string> ReplacePairs, string TemplateText);
    }
}
