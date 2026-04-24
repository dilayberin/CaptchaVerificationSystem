using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;

public class CaptchaChallenge:BaseEntity
{
    public int TargetCategoryId { get; set; } //hedef kategori
    public string QuestionText { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; } //geçerlilik tarihi
    public bool IsSolved { get; set; }
    
    //Her sorunun categorisi vardır
    public Category TargetCategory { get; set; } = null!;
    //Bir captcha içinde birçok görsel var, hangi görseller var?
    public ICollection<CaptchaChallengeImage> CaptchaChallengeImages { get; set; } = new List<CaptchaChallengeImage>();
    //Aynı captcha için birden fazla deneme olabilir 
    public ICollection<CaptchaAttempt> CaptchaAttempts { get; set; } = new List<CaptchaAttempt>();
}