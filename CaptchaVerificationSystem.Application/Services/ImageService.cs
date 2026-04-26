using CaptchaVerificationSystem.Application.DTOs.ImageDtos;
using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Application.Interfaces.Services;

namespace CaptchaVerificationSystem.Application.Services;

public class ImageService:IImageService
{
    private readonly IRepositoryManager _repositoryManager;

    public ImageService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    
    public async Task<List<ImageDto>> GetAllActiveImagesAsync()
    {
        //repository den gelen entity i dto ya dönüştürdük.
        var images = await _repositoryManager.Image.GetAllActiveImagesAsync(false);

        return images.Select(image => new ImageDto
        {
            Id = image.Id,
            FileName = image.FileName,
            FilePath = image.FilePath,
            Width = image.Width,
            Height = image.Height,
            IsActive = image.IsActive
        }).ToList();
    }

    public async Task<ImageDto?> GetByIdAsync(Guid id)
    {
        var image = await _repositoryManager.Image.GetByIdAsync(id, false);

        if (image == null)
            return null;

        return new ImageDto
        {
            Id = image.Id,
            FileName = image.FileName,
            FilePath = image.FilePath,
            Width = image.Width,
            Height = image.Height,
            IsActive = image.IsActive
        };
    }

    public async Task<List<ImageDto>> GetImagesByCategoryIdAsync(Guid categoryId)
    {
        var images = await _repositoryManager.Image.GetImagesByCategoryIdAsync(categoryId, false);

        return images.Select(image => new ImageDto
        {
            Id = image.Id,
            FileName = image.FileName,
            FilePath = image.FilePath,
            Width = image.Width,
            Height = image.Height,
            IsActive = image.IsActive
        }).ToList();
    }
}