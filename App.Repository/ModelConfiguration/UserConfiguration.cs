using App.Core.Models;
using App.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace App.Repository.ModelConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);


            // Seed
            builder.HasData(new User
            {
                Id = 1,
                Name = "app.manager manager1",
                Password = SecureOperations.MD5Hash("!appmanager!"),
                Role = "Manager",
                CreatedOn = DateTime.Now,
                Email = "manager1@manager.com",
                IsEmailConfirmed = true,
                MailConfirmedDate = DateTime.Now.AddHours(1)
            });

            builder.HasData(new User
            {
                Id = 2,
                Name = "app.user user1",
                Password = SecureOperations.MD5Hash("!appuser!"),
                Role = "user",
                CreatedOn = DateTime.Now,
                Email = "user1@manager.com",
                IsEmailConfirmed = false,
                MailConfirmedDate = DateTime.Now.AddHours(2)
            });

            builder.HasData(new User
            {
                Id = 3,
                Name = "app.user user2",
                Password = SecureOperations.MD5Hash("!appuser!"),
                Role = "user",
                CreatedOn = DateTime.Now,
                Email = "user2@manager.com",
                IsEmailConfirmed = true,
                MailConfirmedDate = DateTime.Now.AddHours(3)
            });

            builder.HasData(new User
            {
                Id = 4,
                Name = "app.user user2",
                Password = SecureOperations.MD5Hash("!appuser!"),
                Role = "user",
                CreatedOn = DateTime.Now,
                Email = "user2@manager.com",
                IsEmailConfirmed = false,
            });

            builder.HasData(new User
            {
                Id = 5,
                Name = "app.user user3",
                Password = SecureOperations.MD5Hash("!appuser!"),
                Role = "user",
                CreatedOn = DateTime.Now,
                Email = "user3@manager.com",
                IsEmailConfirmed = false,
            });
        }
    }
}
