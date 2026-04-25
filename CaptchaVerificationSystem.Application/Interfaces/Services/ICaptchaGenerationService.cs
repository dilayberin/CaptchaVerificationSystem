namespace CaptchaVerificationSystem.Application.Interfaces.Services;

public interface ICaptchaGenerationService
{
    Task<Guid> GenerateCaptchaAsync();

}