namespace CaptchaVerificationSystem.Application.DTOs.CategoryDtos;

public class UpdateCategoryDto
{
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }  //Bir kategoriyi tamamen silmek yerine pasif yapıyoruz.
    
}