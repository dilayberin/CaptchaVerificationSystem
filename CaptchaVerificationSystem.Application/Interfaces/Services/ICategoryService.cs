using CaptchaVerificationSystem.Application.DTOs.CategoryDtos;

namespace CaptchaVerificationSystem.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetActiveCategoriesAsync();
    Task<CategoryDto?> GetByIdAsync(Guid id); //category bulunmayabilir o yüzden category?
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
    Task UpdateAsync(Guid id, UpdateCategoryDto dto); //amaç veriyi günc. , DTO döndürmeye gerek yok
}