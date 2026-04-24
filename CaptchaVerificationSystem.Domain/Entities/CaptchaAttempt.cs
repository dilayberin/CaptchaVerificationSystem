using CaptchaVerificationSystem.Domain.Enums;
using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;
//Captcha çözme denemesi
public class CaptchaAttempt:BaseEntity
{
    public int CaptchaChallengeId { get; set; }
    public DateTime AttemptedAt { get; set; }
    public int ResponseTimeMs { get; set; }
    public int CorrectSelectionCount { get; set; }
    public int WrongSelectionCount { get; set; }
    public int MissedCorrectCount { get; set; }
    public decimal Score { get; set; }
    public RiskLevel RiskLevel { get; set; }
    public VerificationResult Result { get; set; }
    
    public CaptchaChallenge CaptchaChallenge { get; set; } = null!;
    public ICollection<CaptchaAttemptSelection> CaptchaAttemptSelections { get; set; } = new List<CaptchaAttemptSelection>();
}
