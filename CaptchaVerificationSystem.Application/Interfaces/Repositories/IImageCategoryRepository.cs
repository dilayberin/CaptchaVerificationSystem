using CaptchaVerificationSystem.Domain.Entities;

namespace CaptchaVerificationSystem.Application.Interfaces.Repositories;

public interface IImageCategoryRepository : IRepositoryBase<ImageCategory>
{
    Task<List<ImageCategory>> GetByImageIdAsync(Guid imageId, bool trackChanges);

}