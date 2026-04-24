using CaptchaVerificationSystem.Domain.Entities;

namespace CaptchaVerificationSystem.Application.Interfaces.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<List<Category>> GetAllAsync(bool trackChanges);
    Task<Category?> GetByIdAsync(Guid id, bool trackChanges);
}