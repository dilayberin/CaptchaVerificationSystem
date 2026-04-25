using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Persistence.Context;
public class CaptchaDbContext:DbContext
{
    public CaptchaDbContext(DbContextOptions<CaptchaDbContext> options)
        : base(options)
    {
    }
    //EF Core la veri tabanının haberleştiği yer , tabloları temsil eden DbSet'ler
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ImageCategory> ImageCategories { get; set; }
    public DbSet<CaptchaChallenge> CaptchaChallenges { get; set; }
    public DbSet<CaptchaChallengeImage> CaptchaChallengeImages { get; set; }
    public DbSet<CaptchaAttempt> CaptchaAttempts { get; set; }
    public DbSet<CaptchaAttemptSelection> CaptchaAttemptSelections { get; set; }
    
    //entity eklenirken, güncellenirken otomatik olarak vt yi günceller.
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
                
            else if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
        return base.SaveChanges();
    }
}