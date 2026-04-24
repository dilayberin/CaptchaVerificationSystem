using CaptchaVerificationSystem.Application.Interfaces.Repositories;
using CaptchaVerificationSystem.Persistance.Context;

namespace CaptchaVerificationSystem.Persistance.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly CaptchaDbContext _context;

    //lazy ile nesne ilk başlatıldığında oluşturulur yani gerektiğinde
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IImageRepository> _imageRepository;
    private readonly Lazy<IImageCategoryRepository> _imageCategoryRepository;

    public RepositoryManager(CaptchaDbContext context)
    {
        _context = context;
        _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(context));
        _imageRepository = new Lazy<IImageRepository>(() => new ImageRepository(context));
        _imageCategoryRepository = new Lazy<IImageCategoryRepository>(() => new ImageCategoryRepository(context));
    }

    //RepositoryManager üzerinden CategoryRepository’ye erişimi sağlar( .value => CategoryRepository nesnesine)
    public ICategoryRepository Category => _categoryRepository.Value;
    public IImageRepository Image => _imageRepository.Value;
    public IImageCategoryRepository ImageCategory => _imageCategoryRepository.Value;

    public async Task SaveAsync() => await _context.SaveChangesAsync();

}

