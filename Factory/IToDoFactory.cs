using Microsoft.AspNetCore.Mvc.Rendering;
using To_DoApp.Domain;
using To_DoApp.Models;

namespace To_DoApp.Factory
{
    public interface IToDoFactory
    {
        ToDoModel CreateToDoModel();
        ToDoListViewModel CreateToDoListViewModel();
        ToDoCreateEditViewModel CreateToDoCreateEditViewModel();
        ToDo CreateToDo(ToDoModel model);

        List<SelectListItem> GetCategoryDropdownItems();
        List<SelectListItem> GetStatusDropdownItems();
    }
}