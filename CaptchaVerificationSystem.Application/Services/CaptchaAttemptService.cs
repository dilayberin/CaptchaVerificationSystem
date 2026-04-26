using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Domain.Entities;
using CaptchaVerificationSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Application.Services;

public class CaptchaAttemptService : ICaptchaAttemptService
{
    private readonly IRepositoryManager _repositoryManager;

    public CaptchaAttemptService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<bool> VerifyCaptchaAsync(Guid challengeId, List<Guid> selectedImageIds, int responseTimeMs)
    {
        // captcha challenge getir
        var challenge = await _repositoryManager.CaptchaChallenge
            .FindByCondition(x => x.Id == challengeId, true)
            .FirstOrDefaultAsync();

        if (challenge == null)
            return false;

        // süre kontrolü
        if (challenge.ExpiresAt < DateTime.UtcNow)
            return false;

        // doğru görselleri getir
        var correctImageIds = await _repositoryManager.CaptchaChallengeImage
            .FindByCondition(x => x.CaptchaChallengeId == challengeId && x.IsCorrect, false)
            .Select(x => x.Id)
            .ToListAsync();

        // seçimleri karşılaştır
        var correctCount = selectedImageIds.Intersect(correctImageIds).Count();
        var wrongCount = selectedImageIds.Except(correctImageIds).Count();
        var missedCorrect = correctImageIds.Except(selectedImageIds).Count();

        bool isSuccess =
            selectedImageIds.Count == correctImageIds.Count &&
            !correctImageIds.Except(selectedImageIds).Any();
        
        // verification sonucu belirle
        VerificationResult result;

        if (isSuccess)
            result = VerificationResult.Human;
        else if (wrongCount >= 3)
            result = VerificationResult.Bot;
        else
            result = VerificationResult.Suspicious;

        // attempt kaydı oluştur
        var attempt = new CaptchaAttempt
        {
            CaptchaChallengeId = challengeId,
            AttemptedAt = DateTime.UtcNow,
            ResponseTimeMs = responseTimeMs,
            CorrectSelectionCount = correctCount,
            WrongSelectionCount = wrongCount,
            MissedCorrectCount = missedCorrect,
            Score = isSuccess ? 1 : 0,
            RiskLevel = isSuccess ? RiskLevel.Low : RiskLevel.Medium,
            Result = result
        };

        _repositoryManager.CaptchaAttempt.Create(attempt);

        // seçilen resimleri kaydet
        foreach (var imageId in selectedImageIds)
        {
            var selection = new CaptchaAttemptSelection
            {
                CaptchaAttemptId = attempt.Id,
                CaptchaChallengeImageId  = imageId
            };

            _repositoryManager.CaptchaAttemptSelection.Create(selection);
        }

        // captcha çözüldü işaretle
        challenge.IsSolved = true;

        await _repositoryManager.SaveAsync();

        return isSuccess;
    }
}