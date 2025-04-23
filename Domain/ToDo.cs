using System.ComponentModel.DataAnnotations;

namespace To_DoApp.Domain
{
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

        [Required(ErrorMessage = "Please select a status")]
        public int  StatusId { get; set; } 
    }
}