
using Microsoft.AspNetCore.Mvc.Rendering;
using To_DoApp.Domain;
using To_DoApp.Factory;
using To_DoApp.Models;
using To_DoApp.Repositories;

namespace To_DoApp.Services
{
    public class ToDoFactory : IToDoFactory
    {
        private readonly IToDoService _todoService;
        private readonly ICategoryService _categoryService;

        public ToDoFactory(IToDoService todoService, ICategoryService categoryService)
        {
            _todoService = todoService;
            _categoryService = categoryService;
        }

        //public IEnumerable<ToDo> GetAllTodos()
        //{
        //    return _todoRepository.GetAll(includeProperties: "Category");
        //}

        //public ToDo GetTodoById(int id)
        //{
        //    return _todoRepository.Get(t => t.Id == id, includeProperties: "Category");
        //}

        public ToDoModel PrepareAndCreateTodoModel(ToDoModel model)
        {

           var categories = _categoryService.GetAllCategories();
            if (categories != null)
            {
                model.CategorySelectList = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId,
                    Text = c.CategoryName
                }).ToList();
            }

            return model;
        }

        public ToDoModel PrepareAndCreateTodo(ToDoModel model)
        {

             var todo = new ToDo()
             {
                 CategoryId = model.CategoryId,
                 Title = model.Title,
                 Description = model.Description,

             }
             //service..

            return model;
        }


        //public void UpdateTodo(ToDo todo)
        //{
        //    _todoRepository.Update(todo);
        //}

        //public void DeleteTodo(int id)
        //{
        //    var todo = GetTodoById(id);
        //    if (todo != null)
        //    {
        //        _todoRepository.Remove(todo);
        //    }
        //}

        //public void ToggleTodoStatus(int id)
        //{
        //    var todo = GetTodoById(id);
        //    if (todo != null)
        //    {
        //        todo.Status = todo.Status switch
        //        {
        //            Models.TaskStatus.Todo => Models.TaskStatus.InProgress,
        //            Models.TaskStatus.InProgress => Models.TaskStatus.Completed,
        //            Models.TaskStatus.Completed => Models.TaskStatus.Todo,
        //            _ => todo.Status
        //        };
        //        UpdateTodo(todo);
        //    }
        //}

        //public IEnumerable<Category> GetAllCategories()
        //{
        //    return _categoryRepository.GetAll();
        //}
    }
}