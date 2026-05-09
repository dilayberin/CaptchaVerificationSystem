using CaptchaVerificationSystem.Domain.Enums;

namespace CaptchaVerificationSystem.Application.DTOs.VerificationDtos;

public class VerifyCaptchaResponseDto
{
    public bool IsSuccess { get; set; }

    public int CorrectSelections { get; set; }

    public int WrongSelections { get; set; }

    public int MissedSelections { get; set; }

    public int Score { get; set; }

    public int ResponseTimeMs { get; set; }

    public RiskLevel RiskLevel { get; set; }

    public VerificationResult Result { get; set; }

    public string Message { get; set; } = string.Empty;
    public List<string> WrongSelectedImages { get; set; } = new();

    public List<string> MissedImages { get; set; } = new();
}