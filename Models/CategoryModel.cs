namespace To_DoApp.Models
{
    public class CategoryModel
    {
        public string CategoryId { get; set; } = Guid.NewGuid().ToString(); 
        public string CategoryName { get; set; } = string.Empty;
    }
}