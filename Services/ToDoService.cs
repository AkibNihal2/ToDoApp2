
using To_DoApp.Models;
using To_DoApp.Repositories;

namespace To_DoApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IRepository<ToDo> _todoRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ToDoService(IRepository<ToDo> todoRepository, IRepository<Category> categoryRepository)
        {
            _todoRepository = todoRepository;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<ToDo> GetAllTodos()
        {
            return _todoRepository.GetAll(includeProperties: "Category");
        }

        public ToDo GetTodoById(int id)
        {
            return _todoRepository.Get(t => t.Id == id, includeProperties: "Category");
        }

        public void CreateTodo(ToDo todo)
        {
            _todoRepository.Add(todo);
        }

        public void UpdateTodo(ToDo todo)
        {
            _todoRepository.Update(todo);
        }

        public void DeleteTodo(int id)
        {
            var todo = GetTodoById(id);
            if (todo != null)
            {
                _todoRepository.Remove(todo);
            }
        }

        public void ToggleTodoStatus(int id)
        {
            var todo = GetTodoById(id);
            if (todo != null)
            {
                todo.Status = todo.Status switch
                {
                    Models.TaskStatus.Todo => Models.TaskStatus.InProgress,
                    Models.TaskStatus.InProgress => Models.TaskStatus.Completed,
                    Models.TaskStatus.Completed => Models.TaskStatus.Todo,
                    _ => todo.Status
                };
                UpdateTodo(todo);
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }
    }
}