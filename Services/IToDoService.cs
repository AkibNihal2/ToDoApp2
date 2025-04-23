using To_DoApp.Models;

namespace To_DoApp.Services
{
    public interface IToDoService
    {
        IEnumerable<ToDo> GetAllTodos();
        ToDo GetTodoById(int id);
        void CreateTodo(ToDo todo);
        void UpdateTodo(ToDo todo);
        void DeleteTodo(int id);
        void ToggleTodoStatus(int id);
        IEnumerable<Category> GetAllCategories();
    }
}