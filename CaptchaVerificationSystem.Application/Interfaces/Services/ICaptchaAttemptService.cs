namespace CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Application.DTOs.VerificationDtos;

public interface ICaptchaAttemptService
{
    Task<VerifyCaptchaResponseDto> VerifyCaptchaAsync(
        Guid challengeId,
        List<Guid> selectedChallengeImageIds,
        int responseTimeMs,
        string? ipAddress,
        string? userAgent);
}