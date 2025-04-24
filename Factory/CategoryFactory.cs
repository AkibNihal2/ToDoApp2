
using To_DoApp.Domain;
using To_DoApp.Models;

namespace To_DoApp.Factory
{
    public class CategoryFactory : ICategoryFactory
    {
        public CategoryModel PrepareCategoryModel()
        {
            return new CategoryModel(); // Can initialize defaults here if needed
        }

        public Category CreateCategory(CategoryModel model)
        {
            return new Category
            {
                CategoryId = Guid.NewGuid().ToString(), // Auto-generate ID
                CategoryName = model.CategoryName
            };
        }
    }
}