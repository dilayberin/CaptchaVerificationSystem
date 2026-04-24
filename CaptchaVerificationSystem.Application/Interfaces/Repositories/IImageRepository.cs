using CaptchaVerificationSystem.Domain.Entities;

namespace CaptchaVerificationSystem.Application.Interfaces.Repositories;

public interface IImageRepository : IRepositoryBase<Image>
{
    Task<List<Image>> GetAllActiveImagesAsync(bool trackChanges);
    Task<Image?> GetByIdAsync(Guid id, bool trackChanges);
    Task<List<Image>> GetImagesByCategoryIdAsync(Guid categoryId, bool trackChanges);
}