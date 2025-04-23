using Microsoft.AspNetCore.Mvc;
using To_DoApp.Models;
using To_DoApp.Repositories;

namespace To_DoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _categoryRepo;

        public CategoryController(IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepo.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            // Initialize a new Category with auto-generated ID
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid().ToString() // Auto-generate ID
            };
            return View(newCategory); // Pass the initialized model
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
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

            var category = _categoryRepo.Get(c => c.CategoryId == id);

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
                _categoryRepo.Update(obj); // need to add Update method to your repository
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

            var category = _categoryRepo.Get(c => c.CategoryId == id);

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
            var category = _categoryRepo.Get(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepo.Remove(category);
            return RedirectToAction("Index");
        }
    }
}