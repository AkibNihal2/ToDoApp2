using To_DoApp.Domain;
using To_DoApp.Models;

namespace To_DoApp.Services
{
    public interface IToDoService
    {
        IEnumerable<ToDoModel> GetAllTodos();
        ToDoModel GetTodoById(int id);
        void CreateTodo(ToDo todo);
        void UpdateTodo(ToDo todo);
        void DeleteTodo(int id);

        IEnumerable<Category> GetAllCategoryEntities();
        IEnumerable<ToDoModel> GetFilteredTodos(string searchString, string statusFilter, string categoryFilter);
    }
}