
using ExamenPOOU1.Database.Entities;
using ExamenPOOU1.Services.Interfaces;
using ExamenPOOU1.Dtos.Categories;
//using System.Xml;



namespace ExamenPOOU1.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        public readonly string _JSON_FILE;

        public CategoriesServices()
        {
            _JSON_FILE = "SeedData/categories.json";
        }

        public async Task<List<CategoryDto>> GetCategoriesListAsync()
        {
            return await ReadCategoriesFromFileAsync();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var categories = await ReadCategoriesFromFileAsync();
            CategoryDto category = categories.FirstOrDefault(c => c.Id == id);

            return category;
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            var categoriesDtos = await ReadCategoriesFromFileAsync();

            var categoryDto = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Description = dto.Description,
                Estado = dto.Estado,
                Prioridad = dto.Prioridad,
            };

            categoriesDtos.Add(categoryDto);

            var categories = categoriesDtos.Select(x => new CategoryEntity
            {
                Id = x.Id,
                Description = x.Description,
                Estado = x.Estado,
                Prioridad = x.Prioridad,

            }).ToList();

            await WriteCategoriesToFileAsync(categories);
            return true;
        }

        public async Task<bool> EditAsync(CategoryEditDto dto, Guid id)
        {
            var categoriesDto = await ReadCategoriesFromFileAsync();

            var existingCategory = categoriesDto.FirstOrDefault(category => category.Id == id);
            if (existingCategory is null)
            {
                return false;
            }


            for (int i = 0; i < categoriesDto.Count; i++)
            {
                if (categoriesDto[i].Id == id)
                {
                    categoriesDto[i].Description = dto.Description;
                    categoriesDto[i].Estado = dto.Estado;
                    categoriesDto[i].Prioridad = dto.Prioridad;
                }
            }

            var categories = categoriesDto.Select(x => new CategoryEntity
            {
                Id = x.Id,
                Description = x.Description,
                Estado = x.Estado,
                Prioridad = x.Prioridad,
            }).ToList();

            await WriteCategoriesToFileAsync(categories);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var categoriesDto = await ReadCategoriesFromFileAsync();
            var categoryToDelete = categoriesDto.FirstOrDefault(x => x.Id == id);

            if (categoryToDelete is null)
            {
                return false;
            }

            categoriesDto.Remove(categoryToDelete);

            var categories = categoriesDto.Select(x => new CategoryEntity
            {
                Id = x.Id,
                Description = x.Description,
                Estado = x.Estado,
                Prioridad = x.Prioridad,

            }).ToList();

            await WriteCategoriesToFileAsync(categories);

            return true;
        }

        private async Task<List<CategoryDto>> ReadCategoriesFromFileAsync()
        {
            if (!File.Exists(_JSON_FILE))
            {
                return new List<CategoryDto>();
            }

            var json = await File.ReadAllTextAsync(_JSON_FILE);
            var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(json);
            var dtos = categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Description = x.Description,
                Estado = x.Estado,
                Prioridad = x.Prioridad,
            }).ToList();
            return dtos;
        }

        private async Task WriteCategoriesToFileAsync(List<CategoryEntity> categories)
        {
            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            if (File.Exists(_JSON_FILE))
            {
                await File.WriteAllTextAsync(_JSON_FILE, json);
            }
        }
    }
}