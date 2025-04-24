using System.ComponentModel.DataAnnotations;

namespace To_DoApp.Domain
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime? DueDate { get; set; }

        [Required]
        public string CategoryId { get; set; } = string.Empty;

        [Required]
        public int StatusId { get; set; }

    }
}