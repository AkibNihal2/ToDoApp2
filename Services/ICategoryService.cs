using To_DoApp.Domain;

namespace To_DoApp.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(string id);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(string id);
    }
}