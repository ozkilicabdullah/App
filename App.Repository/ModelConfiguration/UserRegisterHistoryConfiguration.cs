using App.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repository.ModelConfiguration
{
    public class UserRegisterHistoryConfiguration : IEntityTypeConfiguration<UserRegisterHistory>
    {
        public void Configure(EntityTypeBuilder<UserRegisterHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();

            // Seed Data
            builder.HasData(new UserRegisterHistory
            {
                Id = 1,
                ConfirmDate = DateTime.Now,
                UserId = 1
            });
            builder.HasData(new UserRegisterHistory
            {
                Id = 2,
                ConfirmDate = DateTime.Now,
                UserId = 2
            });
        }
    }
}
