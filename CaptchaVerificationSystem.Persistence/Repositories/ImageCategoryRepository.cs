using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Persistence.Repositories;

public class ImageCategoryRepository: RepositoryBase<ImageCategory>, IImageCategoryRepository
{
    public ImageCategoryRepository(CaptchaDbContext context) : base(context)
    {
    }

    public async Task<List<ImageCategory>> GetByImageIdAsync(Guid imageId, bool trackChanges) =>
        await FindByCondition(x => x.ImageId == imageId, trackChanges)
            .ToListAsync();
}