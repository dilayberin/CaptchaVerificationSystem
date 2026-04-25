using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Persistence.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(CaptchaDbContext context) : base(context)
    {
    }

    public async Task<List<Category>> GetAllAsync(bool trackChanges) =>
        await FindAll(trackChanges).ToListAsync();

    public async Task<Category?> GetByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(x => x.Id == id, trackChanges)
            .FirstOrDefaultAsync();
}