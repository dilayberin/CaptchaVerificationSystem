using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public bool IsActive { get; set; }  //kategori silinmeye gerek kalmadan pasifleşitirilebilir.
    
    // Bir kategori birçok ImageCategory eşleşmesi .
    public ICollection<ImageCategory> ImageCategories { get; set; } = new List<ImageCategory>();
    // Bir kategorinin birden fazla capctha sı olabilir.
    public ICollection<CaptchaChallenge> CaptchaChallenges { get; set; } = new List<CaptchaChallenge>();
}