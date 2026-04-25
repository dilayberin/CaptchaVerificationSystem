using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Persistence.Context;

namespace CaptchaVerificationSystem.Persistence.Repositories;

public class CaptchaAttemptRepository : RepositoryBase<CaptchaAttempt>, ICaptchaAttemptRepository
{
    public CaptchaAttemptRepository(CaptchaDbContext context) : base(context)
    {
    }
}
