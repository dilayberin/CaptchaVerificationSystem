using CaptchaVerificationSystem.Application.Interfaces.Services;

namespace CaptchaVerificationSystem.Application.Services;

public class ServiceManager : IServiceManager
{
    public ICategoryService CategoryService { get; }
    public IImageService ImageService { get; }
    public ICaptchaGenerationService CaptchaGenerationService { get; }
    public ICaptchaAttemptService CaptchaAttemptService { get; }
    
    public ServiceManager(
        ICategoryService categoryService,
        IImageService imageService,
        ICaptchaGenerationService captchaGenerationService,
        ICaptchaAttemptService captchaAttemptService)
    {
        CategoryService = categoryService;
        ImageService = imageService;
        CaptchaGenerationService = captchaGenerationService;
        CaptchaAttemptService = captchaAttemptService;

    }
}