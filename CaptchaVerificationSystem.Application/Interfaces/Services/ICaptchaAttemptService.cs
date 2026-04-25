namespace CaptchaVerificationSystem.Application.Interfaces.Services;

public interface ICaptchaAttemptService
{
    Task<bool> VerifyCaptchaAsync(Guid challengeId, List<Guid> selectedImageIds, int responseTimeMs);
}