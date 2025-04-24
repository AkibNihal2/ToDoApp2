using Microsoft.AspNetCore.Mvc;
using To_DoApp.Domain;
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
            return View(new CategoryModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    CategoryName = model.CategoryName
                };

                _categoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(string id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName
                };

                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(string id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}