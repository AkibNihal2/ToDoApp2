using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace To_DoApp.Models
{
    public enum TaskStatus { Todo, InProgress, Completed }

    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a title")]
        public string Title { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Please provide the description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide the due date")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public string CategoryId { get; set; } = string.Empty;

        [ValidateNever]
        public Category Category { get; set; } = null!;

        [Required(ErrorMessage = "Please select a status")]
        public TaskStatus Status { get; set; } = TaskStatus.Todo; // Default value
    }
}