using GlobalTikectAdminMobile.Models;
using GlobalTikectAdminMobile.Repositories;

namespace GlobalTikectAdminMobile.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
            => await _categoryRepository.GetCategoriesAsync();
    }
}
