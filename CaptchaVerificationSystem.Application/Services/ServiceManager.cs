using CaptchaVerificationSystem.Application.Interfaces.Services;

namespace CaptchaVerificationSystem.Application.Services;

public class ServiceManager : IServiceManager
{
    public ICategoryService CategoryService { get; }
    public IImageService ImageService { get; }

    public ServiceManager(
        ICategoryService categoryService,
        IImageService imageService)
    {
        CategoryService = categoryService;
        ImageService = imageService;
    }
}