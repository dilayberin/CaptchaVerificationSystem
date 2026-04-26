namespace CaptchaVerificationSystem.Application.DTOs.CaptchaDtos;

public class VerifyCaptchaRequestDto
{
    public Guid ChallengeId { get; set; }

    public List<Guid> SelectedImageIds { get; set; } = new();

    public int ResponseTimeMs { get; set; }
}