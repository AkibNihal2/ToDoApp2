using Microsoft.AspNetCore.Mvc;
using To_DoApp.Models;
using To_DoApp.Services; 

namespace To_DoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoService _todoService;

        public HomeController(IToDoService todoService)
        {
            _todoService = todoService;
        }

        public IActionResult Index()
        {
            var todos = _todoService.GetAllTodos();
            return View(todos);
        }

        public IActionResult Create()
        {
            // Initialize a new ToDo object with default values
            var newTodo = new ToDo
            {
                DueDate = DateTime.Today // Set default due date to today
            };

            ViewBag.Categories = _todoService.GetAllCategories();
            return View(newTodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDo obj)
        {
            if (ModelState.IsValid)
            {
                _todoService.CreateTodo(obj);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _todoService.GetAllCategories();
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var todo = _todoService.GetTodoById(id.Value);

            if (todo == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _todoService.GetAllCategories();
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ToDo obj)
        {
            if (ModelState.IsValid)
            {
                _todoService.UpdateTodo(obj);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _todoService.GetAllCategories();
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var todo = _todoService.GetTodoById(id.Value);

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
            var todo = _todoService.GetTodoById(id.Value);

            if (todo == null)
            {
                return NotFound();
            }

            _todoService.DeleteTodo(todo.Id);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleStatus(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            _todoService.ToggleTodoStatus(id.Value);
            return RedirectToAction("Index");
        }
    }
}