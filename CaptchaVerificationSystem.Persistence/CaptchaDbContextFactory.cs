using CaptchaVerificationSystem.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CaptchaVerificationSystem.Persistance;

public class CaptchaDbContextFactory: IDesignTimeDbContextFactory<CaptchaDbContext>
{
    public CaptchaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CaptchaDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=CarpetDb;Username=postgres;Password=123456");

        return new CaptchaDbContext(optionsBuilder.Options);
    }
}