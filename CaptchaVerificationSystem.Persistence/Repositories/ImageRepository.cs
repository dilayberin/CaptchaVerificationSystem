using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Persistance.Repositories;

public class ImageRepository : RepositoryBase<Image>, IImageRepository
{
    public ImageRepository(CaptchaDbContext context) : base(context)
    {
    }

    public async Task<List<Image>> GetAllActiveImagesAsync(bool trackChanges) =>
        await FindByCondition(x => x.IsActive, trackChanges).ToListAsync();

    public async Task<Image?> GetByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(x => x.Id == id, trackChanges)
            .Include(x => x.ImageCategories)
            .FirstOrDefaultAsync();

    public async Task<List<Image>> GetImagesByCategoryIdAsync(Guid categoryId, bool trackChanges) =>
        await FindAll(trackChanges)
            .Where(x => x.ImageCategories.Any(ic => ic.CategoryId == categoryId))
            .ToListAsync();
}