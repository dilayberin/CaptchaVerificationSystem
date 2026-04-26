using CaptchaVerificationSystem.Application.DTOs.CaptchaDtos;

namespace CaptchaVerificationSystem.Application.Interfaces.Services;

public interface ICaptchaGenerationService
{
    Task<CaptchaChallengeDto> GenerateCaptchaAsync();

}