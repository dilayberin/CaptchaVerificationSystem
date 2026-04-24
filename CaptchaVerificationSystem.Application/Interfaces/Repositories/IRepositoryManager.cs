namespace CaptchaVerificationSystem.Application.Interfaces.Repositories;

public interface IRepositoryManager
{
    ICategoryRepository Category { get; }
    IImageRepository Image { get; }
    IImageCategoryRepository ImageCategory { get; }

    Task SaveAsync(); // veri tabanındaki tüm değişiklikleri kalıcı kaydeder.
}