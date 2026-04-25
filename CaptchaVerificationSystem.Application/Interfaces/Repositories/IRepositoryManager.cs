namespace CaptchaVerificationSystem.Application.Interfaces.Repositories;

public interface IRepositoryManager
{
    ICategoryRepository Category { get; }
    IImageRepository Image { get; }
    IImageCategoryRepository ImageCategory { get; }

    ICaptchaChallengeRepository CaptchaChallenge { get; }
    ICaptchaChallengeImageRepository CaptchaChallengeImage { get; }
    ICaptchaAttemptRepository CaptchaAttempt { get; }
    ICaptchaAttemptSelectionRepository CaptchaAttemptSelection { get; }
    

    Task SaveAsync(); // veri tabanındaki tüm değişiklikleri kalıcı kaydeder.
}