using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;

public class ImageCategory:BaseEntity
{
    public Guid ImageId { get; set; }
    public Guid CategoryId { get; set; }
    
    //Bir Image birçok Category ile ilişkili olabilir
    //Bir Category birçok Image ile ilişkili olabilir
    public Image Image { get; set; } = null!;
    public Category Category { get; set; } = null!;
}