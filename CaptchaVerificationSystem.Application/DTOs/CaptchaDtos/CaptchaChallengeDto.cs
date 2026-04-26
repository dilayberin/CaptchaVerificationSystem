namespace CaptchaVerificationSystem.Application.DTOs.CaptchaDtos;

public class CaptchaChallengeDto
{
    public Guid ChallengeId { get; set; }
    public string QuestionText { get; set; } = null!;
    public List<CaptchaImageDto> Images { get; set; } = new();
}

public class CaptchaImageDto
{
    public Guid ImageId { get; set; }
    public string FilePath { get; set; } = null!;
    public int DisplayOrder { get; set; }
}