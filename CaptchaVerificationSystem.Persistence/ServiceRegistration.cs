using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Application.Services;
using CaptchaVerificationSystem.Persistence.Context;

namespace CaptchaVerificationSystem.Persistence;
public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CaptchaDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<ICaptchaGenerationService, CaptchaGenerationService>();
        services.AddScoped<ICaptchaAttemptService, CaptchaAttemptService>();

        return services;
    }
}