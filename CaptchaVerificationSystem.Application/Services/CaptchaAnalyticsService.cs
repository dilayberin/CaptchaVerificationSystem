using CaptchaVerificationSystem.Application.DTOs.AnalyticsDtos;
using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Application.Services;

public class CaptchaAnalyticsService : ICaptchaAnalyticsService
{
    private readonly IRepositoryManager _repositoryManager;

    public CaptchaAnalyticsService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<CaptchaStatisticsDto> GetStatisticsAsync()
    {
        var attempts = await _repositoryManager.CaptchaAttempt
            .FindAll(false)
            .ToListAsync();

        var totalAttempts = attempts.Count;

        if (totalAttempts == 0)
        {
            return new CaptchaStatisticsDto
            {
                TotalAttempts = 0,
                SuccessfulAttempts = 0,
                FailedAttempts = 0,
                HumanCount = 0,
                SuspiciousCount = 0,
                BotCount = 0,
                SuccessRate = 0,
                AverageResponseTimeMs = 0,
                AverageScore = 0
            };
        }

        var successfulAttempts = attempts.Count(x => x.Result == VerificationResult.Human);
        var failedAttempts = totalAttempts - successfulAttempts;

        var humanCount = attempts.Count(x => x.Result == VerificationResult.Human);
        var suspiciousCount = attempts.Count(x => x.Result == VerificationResult.Suspicious);
        var botCount = attempts.Count(x => x.Result == VerificationResult.Bot);

        var successRate = (double)successfulAttempts / totalAttempts * 100;

        var averageResponseTimeMs = attempts.Average(x => x.ResponseTimeMs);
        var averageScore = attempts.Average(x => x.Score);

        return new CaptchaStatisticsDto
        {
            TotalAttempts = totalAttempts,
            SuccessfulAttempts = successfulAttempts,
            FailedAttempts = failedAttempts,
            HumanCount = humanCount,
            SuspiciousCount = suspiciousCount,
            BotCount = botCount,
            SuccessRate = Math.Round(successRate, 2),
            AverageResponseTimeMs = Math.Round(averageResponseTimeMs, 2),
            AverageScore = (double)Math.Round(averageScore, 2)
            
        };
    }
    public async Task<List<AttemptHistoryDto>> GetRecentAttemptsAsync()
    {
        return await _repositoryManager.CaptchaAttempt
            .FindAll(false)
            .OrderByDescending(x => x.AttemptedAt)
            .Take(10)
            .Select(x => new AttemptHistoryDto
            {
                Result = x.Result.ToString(),
                RiskLevel = x.RiskLevel.ToString(),
                Score = x.Score,
                ResponseTimeMs = x.ResponseTimeMs,

                IpAddress = x.IpAddress,
                UserAgent = x.UserAgent,

                AttemptedAt = x.AttemptedAt
            })
            .ToListAsync();
    }
}