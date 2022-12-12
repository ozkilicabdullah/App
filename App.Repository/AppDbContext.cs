using App.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRegisterHistory> UserRegisterHistories { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<UserRegisterHistory>().HasOne(x => x.User).WithMany(x => x.UserRegisterHistories).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<EmailTemplate>().HasOne(x => x.EmailSetting).WithMany(x => x.EmailTemplates).HasForeignKey(x => x.EmailSettingID);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is DbBaseModel entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedOn = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedOn).IsModified = false;
                                entityReference.ModifiedOn = DateTime.Now;
                                break;
                            }
                    }
                }

            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is DbBaseModel entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedOn = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.ModifiedOn = DateTime.Now;
                                Entry(entityReference).Property(x => x.CreatedOn).IsModified = false;
                                break;
                            }
                    }
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

