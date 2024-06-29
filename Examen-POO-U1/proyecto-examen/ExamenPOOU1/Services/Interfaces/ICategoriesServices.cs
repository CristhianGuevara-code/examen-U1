using ExamenPOOU1.Dtos.Categories;

namespace ExamenPOOU1.Services.Interfaces
{
    public class ICategoriesServices
    {
        Task<List<CategoryDto>> GetCategoriesListAsync();
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<bool> CreateAsync(CategoryCreateDto dto);
        Task<bool> EditAsync(CategoryEditDto dto, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
