using Microsoft.EntityFrameworkCore;
using To_DoApp.Data;
using To_DoApp.Domain;
using To_DoApp.Enum;
using To_DoApp.Models;

namespace To_DoApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly ApplicationDbContext _context;

        public ToDoService(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<ToDoModel> GetAllTodos()
        {
            return (from todo in _context.ToDos
                    join category in _context.Categories
                    on todo.CategoryId equals category.CategoryId
                    select new ToDoModel
                    {
                        Id = todo.Id,
                        Title = todo.Title,
                        Description = todo.Description,
                        DueDate = todo.DueDate,
                        CategoryId = todo.CategoryId,
                        CategoryName = category.CategoryName, // From joined table
                        StatusId = todo.StatusId
                    }).ToList();
        }

        public ToDoModel GetTodoById(int id)
        {
            return (from todo in _context.ToDos
                    where todo.Id == id
                    join category in _context.Categories on todo.CategoryId equals category.CategoryId
                    select new ToDoModel
                    {
                        Id = todo.Id,
                        Title = todo.Title,
                        Description = todo.Description,
                        DueDate = todo.DueDate,
                        CategoryId = todo.CategoryId,
                        StatusId = todo.StatusId
                    }).FirstOrDefault();
        }

        public void CreateTodo(ToDo todo)
        {
            _context.ToDos.Add(todo);
            _context.SaveChanges();
        }

        public void UpdateTodo(ToDo todo)
        {
            _context.ToDos.Update(todo);
            _context.SaveChanges();
        }

        public void DeleteTodo(int id)
        {
            var todo = _context.ToDos.Find(id);
            if (todo != null)
            {
                _context.ToDos.Remove(todo);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetAllCategoryEntities()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<ToDoModel> GetFilteredTodos(string searchString, string statusFilter, string categoryFilter)
        {
            var query = from todo in _context.ToDos
                        join category in _context.Categories on todo.CategoryId equals category.CategoryId
                        select new { todo, category };

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.todo.Title.Contains(searchString) ||
                                       x.todo.Description.Contains(searchString));
            }

            // Apply status filter
            if (!string.IsNullOrEmpty(statusFilter) && System.Enum.TryParse<ToDoStatus>(statusFilter, out var status))
            {
                query = query.Where(x => x.todo.StatusId == (int)status);
            }

            // Apply category filter
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                query = query.Where(x => x.todo.CategoryId == categoryFilter);
            }

            return query.Select(x => new ToDoModel
            {
                Id = x.todo.Id,
                Title = x.todo.Title,
                Description = x.todo.Description,
                DueDate = x.todo.DueDate,
                CategoryId = x.todo.CategoryId,
                CategoryName = x.category.CategoryName,
                StatusId = x.todo.StatusId
            }).ToList();
        }
    }
}