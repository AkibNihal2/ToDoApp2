using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoApp2.Enum;
namespace To_DoApp.Models
{  
    public class ToDoModel
    {
        public ToDoModel()
        {
            CategorySelectList = new List<SelectListItem>();
        }
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

        public CategoryModel Category { get; set; } = null!;

        [Required(ErrorMessage = "Please select a status")] 
        public int StatusId { get; set; }
        public TodoStatus TodoStatus { get; set; }
        public IList<SelectListItem> CategorySelectList { get; set; }
    }
}