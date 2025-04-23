
using To_DoApp.Domain;
using To_DoApp.Repositories;

namespace To_DoApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategoryById(string id)
        {
            return _categoryRepository.Get(c => c.CategoryId == id);
        }

        public void CreateCategory(Category category)
        {
            _categoryRepository.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(string id)
        {
            var category = GetCategoryById(id);
            if (category != null)
            {
                _categoryRepository.Remove(category);
            }
        }
    }
}