namespace App.Core.Models
{
    public class EmailTemplate : DbBaseModel
    {
        public string Name { get; set; }
        public SendingContentType SendingContentType { get; set; }
        public string DescriptionHTML { get; set; }
        public string DescriptionText { get; set; }
        public int EmailSettingID { get; set; }
        public virtual EmailSetting EmailSetting { get; set; }

    }
}
