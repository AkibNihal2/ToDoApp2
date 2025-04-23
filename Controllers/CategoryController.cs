using Microsoft.AspNetCore.Mvc;
using To_DoApp.Models;
using To_DoApp.Services; 

namespace To_DoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        public IActionResult Create()
        {
            // Initialize a new Category with auto-generated ID
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid().ToString()
            };
            return View(newCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreateCategory(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string? id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}