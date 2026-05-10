namespace CaptchaVerificationSystem.Application.DTOs.AnalyticsDtos;

public class AttemptHistoryDto
{
    public string Result { get; set; } = string.Empty;

    public string RiskLevel { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public int ResponseTimeMs { get; set; }
    public string? UserAgent { get; set; }
    public string? IpAddress { get; set; }
    public DateTime AttemptedAt { get; set; }
}