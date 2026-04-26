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
        // ilgili ıd ye göre captcha challenge getir
        var challenge = await _repositoryManager.CaptchaChallenge
            .FindByCondition(x => x.Id == challengeId, true)
            .FirstOrDefaultAsync();

        if (challenge == null)
            return false;
        
        if (challenge.IsSolved)
            return false;

        // süre kontrolü , 1 dk yi geçmiş mi
        if (challenge.ExpiresAt < DateTime.UtcNow)
            return false;

        //bu capctha ya ait (x.CaptchaChallengeId ) ve doğru görselleri (x.IsCorrect) getirir SADECE ID lerini.
        var correctImageIds = await _repositoryManager.CaptchaChallengeImage
            .FindByCondition(x => x.CaptchaChallengeId == challengeId && x.IsCorrect, false)
            .Select(x => x.Id)
            .ToListAsync();

        // seçimleri karşılaştır-->doğru seçilenler yanlış seçilenler
        var correctCount = selectedImageIds.Intersect(correctImageIds).Count();
        var wrongCount = selectedImageIds.Except(correctImageIds).Count();
        var missedCorrect = correctImageIds.Except(selectedImageIds).Count(); //yanlış bilinen eleman

        bool isSuccess = 
            selectedImageIds.Count == correctImageIds.Count &&
            !correctImageIds.Except(selectedImageIds).Any();
        
        
        VerificationResult result; //doğrulamanın sonucunu döndürür

        if (isSuccess)
            result = VerificationResult.Human;
        else if (wrongCount >= 3)
            result = VerificationResult.Bot;  //sonra geliştirilecek
        else
            result = VerificationResult.Suspicious;

        // attempt kaydı oluştur yani kullanıcının denemesi kayıt edilir
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

        // kullanıcının seçtiği resimleri kaydet
        foreach (var imageId in selectedImageIds)
        {
            var selection = new CaptchaAttemptSelection
            {
                CaptchaAttemptId = attempt.Id,
                CaptchaChallengeImageId  = imageId
            };

            _repositoryManager.CaptchaAttemptSelection.Create(selection);
        }

        // captcha çözüldü işaretlenir ,aynı captcha tekrar çözülemez
        if (isSuccess)
            challenge.IsSolved = true;

        await _repositoryManager.SaveAsync();

        return isSuccess;
    }
}