using CaptchaVerificationSystem.Application.DTOs.CaptchaDtos;
using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Application.Interfaces.Services;
using CaptchaVerificationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaptchaVerificationSystem.Application.Services;  //!! CAPTCHA ÜRETİMİ !!

/*
 * aktif kategorileri çektik
 * gelen kategorileri karıştırdık
 * doğru resimleri seçtik
 * doğru olanları karıştırıp içinden 3 doğru seçtik
 * tüm resimlerden ID leri doğrularla eşleşmeyen 6 tane seçtik (yanlışları) , karıştırdık
 * doğru ve yanlışları birleştirip karıştırdık
 
 * challange oluşturduk
 * challange de kullanılacak 9 resim için bir liste oluşturduk , resimlerin old listeyi --> caphctaImages tek tek
 dolaştık
 *her resim için CaptchaChallengeImage nesnesi oluşturup vt ye ekledik + kaydettik.
 * frontend e gönderilecek DTO ya çevirdik.
 */

public class CaptchaGenerationService : ICaptchaGenerationService
{
    private readonly IRepositoryManager _repositoryManager; //repository vt ye erişir

    public CaptchaGenerationService(IRepositoryManager repositoryManager) //RepositoryManager a 
    {                                                                     //bağımlılık olmadan erişim
        _repositoryManager = repositoryManager;
    }

    public async Task<CaptchaChallengeDto> GenerateCaptchaAsync()
    {
        // VT den AKTİF kategorileri çek--> vt tarafında bir sql sorgusu
        var categories = await _repositoryManager.Category
            .FindByCondition(x => x.IsActive, false)
            .ToListAsync();

        //VT den gelen category listesini rastgele karıştırır -->SWAGGER TARAFINDA SHUFFLE İŞLEMİ
        var targetCategory = categories
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefault();
        if (targetCategory == null)
            throw new Exception("Aktif kategori bulunamadı.");
        

        // 3 Doğru image  ,ImageCategories de Id-Cat. eşleşmesini Id ile tutuyoruz , o yüzden sorgu "targetCategory.Id"
        var correctImages = await _repositoryManager.Image
            .GetImagesByCategoryIdAsync(targetCategory.Id, false);

        var selectedCorrect = correctImages
            .OrderBy(x => Guid.NewGuid()) //random bi guid değeri atanarak sıralanır(karıştılar)
            .Take(3)//ilk 3 elemanı seçer
            .ToList();

        // Yanlış image ler  (hedef kategoriden olmayanlar)
        var allImages = await _repositoryManager.Image
            .GetAllActiveImagesAsync(false);

        var incorrectImages = allImages
            .Except(correctImages)
            .OrderBy(x => Guid.NewGuid())
            .Take(6)
            .ToList();
        
        /*
         (c => c.Id == x.Id)
        correctImages içindeki herhangi bir resmin Id si şu anki x resminin Id'sine eşit mi?

        correctImages.Any()  -->  correctImages listesinde bu şartı sağlayan bir eleman var mı?
        
        
        var incorrectImages = allImages
             .Except(correctImages)
             .OrderBy(x => Guid.NewGuid())
             .Take(6)
             .ToList();*/
        

        //  doğru ve yanlışları birleştir karıştır
        var captchaImages = selectedCorrect
            .Concat(incorrectImages)
            .OrderBy(x => Guid.NewGuid())
            .ToList();

        // Challenge oluştur
        var challenge = new CaptchaChallenge
        {
            Id = Guid.NewGuid(),
            TargetCategoryId = targetCategory.Id,
            QuestionText = $"İçinde {targetCategory.Name} olan tüm resimleri seçiniz",
            CreatedAt = DateTime.UtcNow,  //DateTime tek başına bir zaman üretmez
            ExpiresAt = DateTime.UtcNow.AddMinutes(1),
            IsSolved = false
        };

        _repositoryManager.CaptchaChallenge.Create(challenge);

        // ChallengeImages oluştur
        var challengeImages = new List<CaptchaChallengeImage>();

        for (int i = 0; i < captchaImages.Count; i++)//captchaImages listesinde resimleri gezer (9) 
        {
            challengeImages.Add(new CaptchaChallengeImage //her resim için bir nesne oluşturup bilgileri vt ye kaydettim
            {
                Id = Guid.NewGuid(),  //CaptchaChallengeImage Id si
                CaptchaChallengeId = challenge.Id,
                ImageId = captchaImages[i].Id,
                DisplayOrder = i,  // ekranda kaçıncı sırada olacak, 9 resimden hangisi
                IsCorrect = selectedCorrect.Any(x => x.Id == captchaImages[i].Id)
            });
        }

        foreach (var item in challengeImages) //NESNELERİ   VT YE KAYDETME
        {
            _repositoryManager.CaptchaChallengeImage.Create(item);
        }

        await _repositoryManager.SaveAsync();

        // captcha yı frontend e gönderilecek DTO haline çevirdik.
        return new CaptchaChallengeDto
        {
            ChallengeId = challenge.Id,
            QuestionText = challenge.QuestionText,
            Images = challengeImages.Select(x => new CaptchaImageDto
            {
                ImageId = x.Id,
                FilePath = captchaImages.First(i => i.Id == x.ImageId).FilePath,
                DisplayOrder = x.DisplayOrder
            }).ToList()
        };
    }
}