using System.ComponentModel;

namespace App.Core.Models
{
    public enum Status
    {
        [Description("Aktif")]
        Active = 0,
        [Description("Pasif")]
        InActive = 1,
        [Description("Silimiş")]
        Removed = 2
    }
    public enum SendingType
    {
        [Description("Email")]
        Email = 0
    }
    public enum SenderProvider
    {
        [Description("SMTP")]
        SMTP = 0
    }
    public enum EmailSendingProtokol
    {
        [Description("SMTP")]
        SMTP = 0
    }
    public enum SendingContentType
    {
        [Description("Mail onay")]
        EmailConfirmation = 0,
        [Description("Şifre Sıfırla")]
        ResetPassword = 1

    }
}
