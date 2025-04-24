using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using To_DoApp.Enum;

namespace To_DoApp.Models
{
    public class ToDoListViewModel
    {
        public List<ToDoModel> ToDoList { get; set; } = new List<ToDoModel>();

        // Filter properties
        public string SearchString { get; set; }
        public string StatusFilter { get; set; }
        public string CategoryFilter { get; set; }

        // Dropdown options for filters
        public List<SelectListItem> AvailableStatuses { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AvailableCategories { get; set; } = new List<SelectListItem>();

        // Dropdown options for forms
        public List<SelectListItem> FormCategories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> FormStatuses { get; set; } = new List<SelectListItem>();
    }
}