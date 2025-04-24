using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using To_DoApp.Domain;
using To_DoApp.Enum;
using To_DoApp.Factory;
using To_DoApp.Models;
using To_DoApp.Services;

namespace To_DoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoService _todoService;
        private readonly IToDoFactory _todoFactory;

        public HomeController(IToDoService todoService, IToDoFactory todoFactory)
        {
            _todoService = todoService;
            _todoFactory = todoFactory;
        }

        public IActionResult Index(ToDoListViewModel filterModel)
        {
            var viewModel = _todoFactory.CreateToDoListViewModel();
            viewModel.ToDoList = _todoService.GetFilteredTodos(
                filterModel.SearchString,
                filterModel.StatusFilter,
                filterModel.CategoryFilter).ToList();

            viewModel.SearchString = filterModel.SearchString;
            viewModel.StatusFilter = filterModel.StatusFilter;
            viewModel.CategoryFilter = filterModel.CategoryFilter;

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(_todoFactory.CreateToDoCreateEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDoCreateEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var todo = _todoFactory.CreateToDo(viewModel.ToDo);
                _todoService.CreateTodo(todo);
                return RedirectToAction("Index");
            }


            viewModel.Categories = _todoFactory.GetCategoryDropdownItems();
            viewModel.Statuses = _todoFactory.GetStatusDropdownItems();
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }

            var viewModel = _todoFactory.CreateToDoCreateEditViewModel();
            viewModel.ToDo.Id = todo.Id;
            viewModel.ToDo.Title = todo.Title;
            viewModel.ToDo.Description = todo.Description;
            viewModel.ToDo.DueDate = todo.DueDate;
            viewModel.ToDo.CategoryId = todo.CategoryId;
            viewModel.ToDo.StatusId = todo.StatusId;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ToDoCreateEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var todo = new ToDo
                {
                    Id = viewModel.ToDo.Id,
                    Title = viewModel.ToDo.Title,
                    Description = viewModel.ToDo.Description,
                    DueDate = viewModel.ToDo.DueDate,
                    CategoryId = viewModel.ToDo.CategoryId,
                    StatusId = viewModel.ToDo.StatusId
                };

                _todoService.UpdateTodo(todo);
                return RedirectToAction("Index");
            }

            
            viewModel.Categories = _todoFactory.GetCategoryDropdownItems();
            viewModel.Statuses = _todoFactory.GetStatusDropdownItems();
            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _todoService.DeleteTodo(id);
            return RedirectToAction("Index");
        }
    }
}