using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using To_DoApp.Enum;

namespace To_DoApp.Models
{
    public class ToDoCreateEditViewModel
    {
        public ToDoModel ToDo { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
    }
}