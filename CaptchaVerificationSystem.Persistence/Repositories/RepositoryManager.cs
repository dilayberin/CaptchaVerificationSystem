using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Persistence.Context;

namespace CaptchaVerificationSystem.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly CaptchaDbContext _context;

    //lazy ile nesne ilk başlatıldığında oluşturulur yani gerektiğinde
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IImageRepository> _imageRepository;
    private readonly Lazy<IImageCategoryRepository> _imageCategoryRepository;
    private readonly Lazy<ICaptchaChallengeRepository> _captchaChallengeRepository;
    private readonly Lazy<ICaptchaChallengeImageRepository> _captchaChallengeImageRepository;
    private readonly Lazy<ICaptchaAttemptRepository> _captchaAttemptRepository;
    private readonly Lazy<ICaptchaAttemptSelectionRepository> _captchaAttemptSelectionRepository;

    public RepositoryManager(CaptchaDbContext context)
    {
        _context = context;
        _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(context));
        _imageRepository = new Lazy<IImageRepository>(() => new ImageRepository(context));
        _imageCategoryRepository = new Lazy<IImageCategoryRepository>(() => new ImageCategoryRepository(context));
        _captchaChallengeRepository = new Lazy<ICaptchaChallengeRepository>(() =>
            new CaptchaChallengeRepository(context));
        _captchaChallengeImageRepository =
            new Lazy<ICaptchaChallengeImageRepository>(() =>
                new CaptchaChallengeImageRepository(context));
        _captchaAttemptRepository =
            new Lazy<ICaptchaAttemptRepository>(() => new CaptchaAttemptRepository(context));

        _captchaAttemptSelectionRepository =
            new Lazy<ICaptchaAttemptSelectionRepository>(() => new CaptchaAttemptSelectionRepository(context));
    }

    //RepositoryManager üzerinden CategoryRepository’ye erişimi sağlar( .value => CategoryRepository nesnesine)
    public ICategoryRepository Category => _categoryRepository.Value;
    public IImageRepository Image => _imageRepository.Value;
    public IImageCategoryRepository ImageCategory => _imageCategoryRepository.Value;
    public ICaptchaChallengeRepository CaptchaChallenge 
        => _captchaChallengeRepository.Value;
    public ICaptchaChallengeImageRepository CaptchaChallengeImage
        => _captchaChallengeImageRepository.Value;
    public ICaptchaAttemptRepository CaptchaAttempt
        => _captchaAttemptRepository.Value;
    public ICaptchaAttemptSelectionRepository CaptchaAttemptSelection
        => _captchaAttemptSelectionRepository.Value;
    
    public async Task SaveAsync() => await _context.SaveChangesAsync();

}

