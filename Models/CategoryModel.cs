using System.ComponentModel.DataAnnotations;

namespace To_DoApp.Models
{
    public class CategoryModel
    {
        public string CategoryId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; } = string.Empty;
    }
}