using CaptchaVerificationSystem.Application.DTOs.CaptchaDtos;
using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Api.FileServices;

public class CaptchaFileService
{
    private readonly IWebHostEnvironment _env;
    private readonly IRepositoryManager _repositoryManager;
    private readonly Random _random = new();

    public CaptchaFileService(IWebHostEnvironment env, IRepositoryManager repositoryManager)
    {
        _env = env;
        _repositoryManager = repositoryManager;
    }

    public async Task<CaptchaChallengeDto> GenerateCaptchaAsync()
    {
        string basePath = Path.Combine(_env.WebRootPath, "captcha-images");

        var categories = Directory.GetDirectories(basePath)
                                  .Select(Path.GetFileName)
                                  .ToList();

        var targetCategory = categories[_random.Next(categories.Count)];
        var categoryEntity = await _repositoryManager.Category
            .FindByCondition(x => x.Name.ToLower() == targetCategory.ToLower(), false)
            .FirstOrDefaultAsync();

        if (categoryEntity == null)
            throw new Exception($"Kategori DB'de yok: {targetCategory}");

        var correctImages = Directory.GetFiles(Path.Combine(basePath, targetCategory))
                                     .OrderBy(x => _random.Next())
                                     .Take(3)
                                     .ToList();

        var otherCategories = categories.Where(c => c != targetCategory).ToList();

        var wrongImages = new List<string>();

        foreach (var cat in otherCategories)
        {
            var imgs = Directory.GetFiles(Path.Combine(basePath, cat))
                                .OrderBy(x => _random.Next())
                                .Take(2)
                                .ToList();

            wrongImages.AddRange(imgs);

            if (wrongImages.Count >= 6)
                break;
        }

        wrongImages = wrongImages.Take(6).ToList();

        var allImages = correctImages.Concat(wrongImages)
                                     .OrderBy(x => _random.Next())
                                     .ToList();

        // 🔥 CHALLENGE OLUŞTUR
        var challenge = new CaptchaChallenge
        {
            Id = Guid.NewGuid(),
            TargetCategoryId = categoryEntity.Id, 
            QuestionText = $"Select all images with {categoryEntity.DisplayName ?? categoryEntity.Name}",
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            IsSolved = false
        };
        _repositoryManager.CaptchaChallenge.Create(challenge);

        var imageDtos = new List<CaptchaImageDto>();

        for (int i = 0; i < allImages.Count; i++)
        {
            var img = allImages[i];
            string category = new DirectoryInfo(Path.GetDirectoryName(img)!).Name;

            bool isCorrect = correctImages.Contains(img);

            var challengeImage = new CaptchaChallengeImage
            {
                Id = Guid.NewGuid(),
                CaptchaChallengeId = challenge.Id,
                IsCorrect = isCorrect,
                DisplayOrder = i
            };

            _repositoryManager.CaptchaChallengeImage.Create(challengeImage);

            imageDtos.Add(new CaptchaImageDto
            {
                ImageId = challengeImage.Id, // 🔥 ÇOK ÖNEMLİ
                FilePath = $"/captcha-images/{category}/{Path.GetFileName(img)}",
                DisplayOrder = i
            });
        }

        await _repositoryManager.SaveAsync();

        return new CaptchaChallengeDto
        {
            ChallengeId = challenge.Id,
            QuestionText = challenge.QuestionText,
            Images = imageDtos
        };
    }
}