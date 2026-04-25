using CaptchaVerificationSystem.Domain.Models;

namespace CaptchaVerificationSystem.Domain.Entities;

public class CaptchaChallengeImage:BaseEntity
{
    public Guid CaptchaChallengeId { get; set; }
    public Guid ImageId { get; set; }
    public int DisplayOrder { get; set; } //resmin ekranda gösterildiği yer
    public bool IsCorrect { get; set; }  //resim doğru kategoriden mi
    
    //Bir captcha içinde birden fazla resim var, görselin ait olduğu captcha
    public CaptchaChallenge CaptchaChallenge { get; set; } = null!;
    //Bir resim birden fazla captcha’da kullanılabilir.
    public Image Image { get; set; } = null!;
    //kullanıcı resmi seçmiş mi
    public ICollection<CaptchaAttemptSelection> CaptchaAttemptSelections { get; set; } = new List<CaptchaAttemptSelection>();
}