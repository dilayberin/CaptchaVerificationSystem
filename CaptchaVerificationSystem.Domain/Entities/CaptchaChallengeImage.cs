using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;

public class CaptchaChallengeImage : BaseEntity
{
    public Guid CaptchaChallengeId { get; set; }

    public bool IsCorrect { get; set; }

    public int DisplayOrder { get; set; }

    public CaptchaChallenge CaptchaChallenge { get; set; } = null!;

    public ICollection<CaptchaAttemptSelection> CaptchaAttemptSelections { get; set; } = new List<CaptchaAttemptSelection>();
}