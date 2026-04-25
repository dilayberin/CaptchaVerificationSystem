namespace CaptchaVerificationSystem.Application.DTOs.CategoryDtos;

public class CategoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public bool IsActive { get; set; }
}