using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using To_DoApp.Factory;
using To_DoApp.Models;
using To_DoApp.Services; 

namespace To_DoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoService _todoService;
        private readonly IToDoFactory _toDoFactory;

        public HomeController(IToDoService todoService, IToDoFactory toDoFactory)
        {
            _todoService = todoService;
            _toDoFactory = toDoFactory;
        }

        public IActionResult Index(string searchString, string statusFilter, string categoryFilter)
        {
            // Get all todos with categories included
            var todos = _todoService.GetAllTodos();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                todos = todos.Where(t =>
                    t.Title.ToLower().Contains(searchString) ||
                    t.Description.ToLower().Contains(searchString));
            }

            // Apply status filter if provided
            if (!string.IsNullOrEmpty(statusFilter))
            {
                if (Enum.TryParse(statusFilter, out Models.TaskStatus status))
                {
                    todos = todos.Where(t => t.Status == status);
                }
            }

            // Apply category filter if provided
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                todos = todos.Where(t => t.CategoryId == categoryFilter);
            }

            // Prepare dropdown data
            ViewBag.StatusList = Enum.GetValues(typeof(Models.TaskStatus))
                .Cast<Models.TaskStatus>()
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString()
                });

            ViewBag.CategoryList = _todoService.GetAllCategories()
                .Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId
                });

            return View(todos);
        }

        public IActionResult Create()
        {
            var newTodo = new ToDoModel();
            _toDoFactory.PrepareAndCreateTodoModel(newTodo);

            return View(newTodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDoModel model)
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