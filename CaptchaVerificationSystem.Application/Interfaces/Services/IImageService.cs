using CaptchaVerificationSystem.Application.DTOs.ImageDtos;

namespace CaptchaVerificationSystem.Application.Interfaces.Services;

public interface IImageService
{
    Task<List<ImageDto>> GetAllActiveImagesAsync();
    Task<ImageDto?> GetByIdAsync(Guid id);
    Task<List<ImageDto>> GetImagesByCategoryIdAsync(Guid categoryId);
}