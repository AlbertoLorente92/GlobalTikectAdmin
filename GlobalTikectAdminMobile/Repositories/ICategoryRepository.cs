using GlobalTikectAdminMobile.Models;

namespace GlobalTikectAdminMobile.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetCategoriesAsync();
    }
}
