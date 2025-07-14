using GlobalTikectAdminMobile.Models;

namespace GlobalTikectAdminMobile.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetCategoriesAsync();
    }
}
