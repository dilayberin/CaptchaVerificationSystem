namespace CaptchaVerificationSystem.Application.DTOs.AnalyticsDtos;

public class CaptchaStatisticsDto
{
    public int TotalAttempts { get; set; }

    public int SuccessfulAttempts { get; set; }

    public int FailedAttempts { get; set; }

    public int HumanCount { get; set; }

    public int SuspiciousCount { get; set; }

    public int BotCount { get; set; }

    public double SuccessRate { get; set; }

    public double AverageResponseTimeMs { get; set; }

    public double AverageScore { get; set; }
}