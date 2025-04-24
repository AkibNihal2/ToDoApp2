
using To_DoApp.Domain;
using To_DoApp.Models;

namespace To_DoApp.Factory
{
    public interface ICategoryFactory
    {
        CategoryModel PrepareCategoryModel();
        Category CreateCategory(CategoryModel model);
    }
}