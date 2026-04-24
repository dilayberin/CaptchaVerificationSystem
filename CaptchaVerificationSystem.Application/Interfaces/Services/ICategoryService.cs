using CaptchaVerificationSystem.Application.DTOs.CategoryDtos;

namespace CaptchaVerificationSystem.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(Guid id);
}