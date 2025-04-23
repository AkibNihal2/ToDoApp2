using To_DoApp.Domain;
using To_DoApp.Models;

namespace To_DoApp.Factory
{
    public interface IToDoFactory
    {
        ToDoModel PrepareAndCreateTodoModel(ToDoModel model);

    }
}