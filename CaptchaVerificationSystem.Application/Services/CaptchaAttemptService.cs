using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Application.DTOs.VerificationDtos;
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

    public async Task<VerifyCaptchaResponseDto> VerifyCaptchaAsync(Guid challengeId, List<Guid> selectedChallengeImageIds, int responseTimeMs)    {
        // ilgili ıd ye göre captcha challenge getir
        var challenge = await _repositoryManager.CaptchaChallenge
            .FindByCondition(x => x.Id == challengeId, true)
            .FirstOrDefaultAsync();

        if (challenge == null)
        {
            return new VerifyCaptchaResponseDto
            {
                IsSuccess = false,
                Score = 0,
                RiskLevel = RiskLevel.High,
                Result = VerificationResult.Bot,
                Message = "Captcha bulunamadı."
            };
        }
            
        
        if (challenge.IsSolved)
        {
            return new VerifyCaptchaResponseDto
            {
                IsSuccess = false,
                Score = 0,
                RiskLevel = RiskLevel.High,
                Result = VerificationResult.Suspicious,
                Message = "Bu captcha daha önce çözüldü!"
            };
        }

        // süre kontrolü , 1 dk yi geçmiş mi
        if (challenge.ExpiresAt < DateTime.UtcNow)
        {
            return new VerifyCaptchaResponseDto
            {
                IsSuccess = false,
                Score = 0,
                RiskLevel = RiskLevel.High,
                Result = VerificationResult.Suspicious,
                Message = "Captcha süresi doldu!"
            };
        }

        //bu capctha ya ait (x.CaptchaChallengeId ) ve doğru görselleri (x.IsCorrect) getirir SADECE ID lerini.
        var correctImageIds = await _repositoryManager.CaptchaChallengeImage
            .FindByCondition(x => x.CaptchaChallengeId == challengeId && x.IsCorrect, false)
            .Select(x => x.Id)
            .ToListAsync();

        // seçimleri karşılaştır-->doğru seçilenler yanlış seçilenler
        var correctCount = selectedChallengeImageIds.Intersect(correctImageIds).Count();
        var wrongCount = selectedChallengeImageIds.Except(correctImageIds).Count();
        var missedCorrect = correctImageIds.Except(selectedChallengeImageIds).Count(); //yanlış bilinen eleman
        
        var wrongSelectedIds = selectedChallengeImageIds
            .Except(correctImageIds)
            .ToList();

        var missedImageIds = correctImageIds
            .Except(selectedChallengeImageIds)
            .ToList();

        bool isSuccess =
            wrongCount == 0 &&
            missedCorrect == 0 &&
            selectedChallengeImageIds.Count == correctImageIds.Count;
        
        
        int score = 100; //Kullanıcı başlangıçta güvenilir kabul edilir

        score -= wrongCount * 20;
        score -= missedCorrect * 10;
        
        int overSelection = selectedChallengeImageIds.Count - correctImageIds.Count; //gereğinde fazla seçim 
        if (overSelection > 1)                                                   //yapılmış mı? bot 3 resim yerine
        {                                                                   //garanti olması için 5 7 vs. seçer
            score -= overSelection * 15;
        }

        if (responseTimeMs < 1000)
            score -= 30;
        score = Math.Max(score, 0);
        
        RiskLevel riskLevel;

        
        if (score >= 80)
            riskLevel = RiskLevel.Low;
        else if (score >= 50)
            riskLevel = RiskLevel.Medium;
        else
            riskLevel = RiskLevel.High;
        
        VerificationResult result; //doğrulamanın sonucunu döndürür


        if (isSuccess && riskLevel == RiskLevel.Low)
            result = VerificationResult.Human;
        else if (riskLevel == RiskLevel.High)
            result = VerificationResult.Bot;      //Sonra geliştirilecek!!
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
            Score = score,
            RiskLevel = riskLevel,
            Result = result
        };

        _repositoryManager.CaptchaAttempt.Create(attempt);

        // kullanıcının seçtiği resimleri kaydet
        foreach (var imageId in selectedChallengeImageIds)
        {
            var selection = new CaptchaAttemptSelection
            {
                CaptchaAttemptId = attempt.Id,
                CaptchaChallengeImageId = imageId,
                IsSelected = true,
                IsCorrectSelection = correctImageIds.Contains(imageId)
            };

            _repositoryManager.CaptchaAttemptSelection.Create(selection);
        }

        // captcha çözüldü işaretlenir ,aynı captcha tekrar çözülemez
        if (isSuccess)
            challenge.IsSolved = true;

        await _repositoryManager.SaveAsync();

        return new VerifyCaptchaResponseDto
        {
            IsSuccess = isSuccess,
            CorrectSelections = correctCount,
            WrongSelections = wrongCount,
            MissedSelections = missedCorrect,
            Score = score,
            ResponseTimeMs = responseTimeMs,
            RiskLevel = riskLevel,
            Result = result,
            Message = isSuccess
                ? "Captcha doğrulandı!"
                : "Captcha doğrulaması başarısız!"
        };
    }
}