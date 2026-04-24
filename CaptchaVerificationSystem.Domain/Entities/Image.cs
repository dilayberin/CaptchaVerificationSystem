using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;

public class Image:BaseEntity
{
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsActive { get; set; }
    
    //hangi kategori?
    public ICollection<ImageCategory> ImageCategories { get; set; } = new List<ImageCategory>();
    //hangi capthcalarda kullanılmış
    public ICollection<CaptchaChallengeImage> CaptchaChallengeImages { get; set; } = new List<CaptchaChallengeImage>();
}