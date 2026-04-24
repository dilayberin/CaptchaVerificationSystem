namespace CaptchaVerificationSystem.Application.DTOs.ImageDtos;

public class ImageDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsActive { get; set; }
}