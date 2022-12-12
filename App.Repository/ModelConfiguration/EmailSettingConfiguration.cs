using App.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repository.ModelConfiguration
{
    public class EmailSettingConfiguration : IEntityTypeConfiguration<EmailSetting>
    {
        public void Configure(EntityTypeBuilder<EmailSetting> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Password).HasMaxLength(40);
            builder.Property(x => x.SenderName).HasMaxLength(90);
            builder.Property(x => x.SenderEmail).HasMaxLength(90);
            builder.Property(x => x.UserName).HasMaxLength(90);
            builder.Property(x => x.ApiKey).HasMaxLength(90);
            builder.Property(x => x.ApiTransactionCode).HasMaxLength(90);
            builder.Property(x => x.Port).IsRequired();

            builder.HasData(new EmailSetting
            {
                Id = 1,
                Name = "Genel Smtp",
                SenderName= "digiapplogin@outlook.com.tr",
                SenderEmail= "digiapplogin@outlook.com.tr",
                UserName= "digiapplogin@outlook.com.tr",
                Password= "!applogin!99",
                Port= 587,
                Host= "smtp.office365.com",
                EnableSsl= true,
                ApiKey="123",
                ApiTransactionCode="123",
                CreatedOn=DateTime.Now,
                SendingProtokol= EmailSendingProtokol.SMTP
            });

        }

    }
}
