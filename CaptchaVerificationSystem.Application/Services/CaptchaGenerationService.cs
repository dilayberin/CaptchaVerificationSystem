using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Domain.Entities;

namespace CaptchaVerificationSystem.Application.Services;

public class CaptchaGenerationService:ICaptchaGenerationService
{
    
    private readonly IRepositoryManager _repositoryManager;

    public CaptchaGenerationService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Guid> GenerateCaptchaAsync()
    {
        var challenge = new CaptchaChallenge
        {
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(2),
            IsSolved = false
        };

        _repositoryManager.CaptchaChallenge.Create(challenge);
        await _repositoryManager.SaveAsync();

        return challenge.Id;
    }
}