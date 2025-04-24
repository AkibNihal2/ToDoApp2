using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using To_DoApp.Enum;

namespace To_DoApp.Models
{
    public class ToDoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Due date is required")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string CategoryId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        public int StatusId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}