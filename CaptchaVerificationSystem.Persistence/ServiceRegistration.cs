using CaptchaVerificationSystem.Persistance.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Persistance;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CaptchaDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        /*
         * services.AddScoped<IImageReadRepository,ImageReadRepository>();
       services.AddScoped<IImageWriteRepository, ImageWriteRepository>();
         *
         */
        return services;
    }


}