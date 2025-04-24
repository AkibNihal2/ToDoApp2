using Microsoft.EntityFrameworkCore;
using To_DoApp.Data;
using To_DoApp.Domain;
using To_DoApp.Models;

namespace To_DoApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return _context.Categories
                .Select(c => new CategoryModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToList();
        }

        public CategoryModel GetCategoryById(string id)
        {
            return _context.Categories
                .Where(c => c.CategoryId == id)
                .Select(c => new CategoryModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .FirstOrDefault();
        }

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(string id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}