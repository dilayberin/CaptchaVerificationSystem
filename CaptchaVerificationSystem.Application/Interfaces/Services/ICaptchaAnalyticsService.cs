using CaptchaVerificationSystem.Application.DTOs.AnalyticsDtos;

namespace CaptchaVerificationSystem.Application.Interfaces.Services;

public interface ICaptchaAnalyticsService
{
    Task<CaptchaStatisticsDto> GetStatisticsAsync();
}