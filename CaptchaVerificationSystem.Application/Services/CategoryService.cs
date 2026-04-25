using CaptchaVerificationSystem.Application.DTOs.CategoryDtos;
using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Application.Services;

public class CategoryService : ICategoryService
{

    private readonly IRepositoryManager _repositoryManager; //tüm repositoryleri tek noktadan yönetmek 
    //repository dependency

    public CategoryService(IRepositoryManager repositoryManager) //servis repository üzerinden veritabanına erişir,
    {
        // servis doğrudan DBContext'e (vt ye) bağlı olmasın
        _repositoryManager = repositoryManager;
    }

    public async Task<IEnumerable<CategoryDto>> GetActiveCategoriesAsync()
    {
        var categories = await _repositoryManager.Category
            .FindByCondition(x => x.IsActive, false)
            .ToListAsync();

        return categories.Select(category => new CategoryDto //vt sorgusu
        {
            Id = category.Id,
            Name = category.Name,
            DisplayName = category.DisplayName,
            IsActive = category.IsActive
        });
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid id)
    {
        var category = await _repositoryManager.Category
            .FindByCondition(x => x.Id == id, false)
            .FirstOrDefaultAsync();

        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            DisplayName = category.DisplayName,
            IsActive = category.IsActive
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(), //rastgele Id üretir
            Name = dto.Name,
            DisplayName = dto.DisplayName,
            IsActive = true
        };

        _repositoryManager.Category.Create(category); //Category entity’sini DbContext'e ekler

        await _repositoryManager.SaveAsync(); //asıl INSERT işlemini bu satır yapar

        var result = new CategoryDto
        {
            Id = category.Id,                    //Category entity->CategoryDto
            Name = category.Name,
            DisplayName = dto.DisplayName,
            IsActive = true
        };

        return result;
    }

    public async Task UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _repositoryManager.Category
            .FindByCondition(x => x.Id == id, true)
            .FirstOrDefaultAsync();  //şarta uyan ilk elemanı getir,yoksa null döndür

        if (category == null)
            throw new Exception("Category not found");

        category.Name = dto.Name;
                                                      //DTO ya dönüştürme
        await _repositoryManager.SaveAsync();
    }

}