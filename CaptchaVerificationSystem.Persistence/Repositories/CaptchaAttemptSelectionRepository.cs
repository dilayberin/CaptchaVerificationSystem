using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Persistence.Context;

namespace CaptchaVerificationSystem.Persistence.Repositories;

public class CaptchaAttemptSelectionRepository : RepositoryBase<CaptchaAttemptSelection>, ICaptchaAttemptSelectionRepository
{
    public CaptchaAttemptSelectionRepository(CaptchaDbContext context) : base(context)
    {
    }
}
