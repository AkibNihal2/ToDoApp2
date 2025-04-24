using To_DoApp.Domain;
using To_DoApp.Models;

namespace To_DoApp.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();
        CategoryModel GetCategoryById(string id);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(string id);

    }
}