using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;

public class CaptchaAttemptSelection : BaseEntity  ////her resim için Id, capchta daki 9 resimden hangisi seçildi veya seçilmedi,seçildi mi
{
    public Guid CaptchaAttemptId { get; set; }

    public Guid CaptchaChallengeImageId { get; set; }

    public bool IsSelected { get; set; }

    public CaptchaAttempt CaptchaAttempt { get; set; } = null!;

    public CaptchaChallengeImage CaptchaChallengeImage { get; set; } = null!;
}