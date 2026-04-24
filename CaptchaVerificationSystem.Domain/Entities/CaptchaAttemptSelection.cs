using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;
//her resim için Id, capchta daki 9 resimden hangisi seçildi veya seçilmedi,seçildi mi
public class CaptchaAttemptSelection:BaseEntity
{
    public int CaptchaAttemptId { get; set; }
    public int CaptchaChallengeImageId { get; set; }
    public bool IsSelected { get; set; }
    
    public CaptchaAttempt CaptchaAttempt { get; set; } = null!;
    public CaptchaChallengeImage CaptchaChallengeImage { get; set; } = null!;
}