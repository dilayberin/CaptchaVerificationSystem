using System.Linq.Expressions;
using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Persistence.Context;

namespace CaptchaVerificationSystem.Persistence.Repositories;

public class CaptchaChallengeImageRepository 
    : RepositoryBase<CaptchaChallengeImage>, ICaptchaChallengeImageRepository
{
    public CaptchaChallengeImageRepository(CaptchaDbContext context) 
        : base(context)
    {
    }
    
}