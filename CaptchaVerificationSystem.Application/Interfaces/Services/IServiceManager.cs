namespace CaptchaVerificationSystem.Application.Interfaces.Services;
//servislerin tek noktadan yönetimi
public interface IServiceManager
{
    ICategoryService CategoryService { get; }
    IImageService ImageService { get; }
    ICaptchaGenerationService CaptchaGenerationService { get; }
    ICaptchaAttemptService CaptchaAttemptService { get; }
    ICaptchaAnalyticsService CaptchaAnalyticsService { get; }
}