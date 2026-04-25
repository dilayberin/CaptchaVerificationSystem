using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Persistence.Context;

namespace CaptchaVerificationSystem.Persistence.Repositories;

public class CaptchaChallengeRepository: RepositoryBase<CaptchaChallenge>, ICaptchaChallengeRepository
{
    public CaptchaChallengeRepository(CaptchaDbContext context) : base(context)
    {
    }
}
