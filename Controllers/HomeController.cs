using Microsoft.AspNetCore.Mvc;
using To_DoApp.Models;
using To_DoApp.Repositories;

namespace To_DoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<ToDo> _todoRepo;
        private readonly IRepository<Category> _categoryRepo;

        public HomeController(IRepository<ToDo> todoRepo, IRepository<Category> categoryRepo)
        {
            _todoRepo = todoRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var todos = _todoRepo.GetAll(includeProperties: "Category");
            return View(todos);
        }

        public IActionResult Create()
        {
            // Initialize a new ToDo object with default values
            var newTodo = new ToDo
            {
                DueDate = DateTime.Today // Set default due date to today
            };

            ViewBag.Categories = _categoryRepo.GetAll();
            return View(newTodo); // Pass the initialized model
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDo obj)
        {
            if (ModelState.IsValid)
            {
                _todoRepo.Add(obj);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryRepo.GetAll();
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var todo = _todoRepo.Get(t => t.Id == id, includeProperties: "Category");

            if (todo == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _categoryRepo.GetAll();
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ToDo obj)
        {
            if (ModelState.IsValid)
            {
                _todoRepo.Update(obj); // need to add Update method to your repository
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryRepo.GetAll();
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var todo = _todoRepo.Get(t => t.Id == id, includeProperties: "Category");

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var todo = _todoRepo.Get(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            _todoRepo.Remove(todo);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleStatus(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var todo = _todoRepo.Get(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            // Cycle through statuses
            todo.Status = todo.Status switch
            {
                Models.TaskStatus.Todo => Models.TaskStatus.InProgress,
                Models.TaskStatus.InProgress => Models.TaskStatus.Completed,
                Models.TaskStatus.Completed => Models.TaskStatus.Todo,
                _ => todo.Status
            };

            _todoRepo.Update(todo); // need to add Update method to your repository
            return RedirectToAction("Index");
        }
    }
}