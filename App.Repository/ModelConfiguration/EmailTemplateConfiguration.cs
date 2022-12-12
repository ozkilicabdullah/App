using App.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repository.ModelConfiguration
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);

            builder.HasData(new EmailTemplate
            {
                Id = 1,
                Name = "New member",
                SendingContentType = SendingContentType.EmailConfirmation,
                DescriptionHTML = "<p>Üyeliğinizi tamamlamak için son adım. Üyeliğinizi onaylamak için <a href='MailConfirmationValidationSecretKey'>buraya tıklayınız.</a></p>",
                DescriptionText = "Üyelik onay maili",
                EmailSettingID = 1,
                CreatedOn = DateTime.Now
            });
            builder.HasData(new EmailTemplate
            {
                Id = 2,
                Name = "Foget my password",
                SendingContentType = SendingContentType.ResetPassword,
                DescriptionHTML = "<p>Paraloanızı sıfırlamak için <a href='ResetPasswordSecretKey'>buraya tıklayınız.</a></p>",
                DescriptionText = "Şifremi Unuttum",
                EmailSettingID = 1,
                CreatedOn = DateTime.Now
            });
        }
    }
}
