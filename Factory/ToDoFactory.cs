using Microsoft.AspNetCore.Mvc.Rendering;
using To_DoApp.Domain;
using To_DoApp.Enum;
using To_DoApp.Models;
using To_DoApp.Services;
using System;

namespace To_DoApp.Factory
{
    public class ToDoFactory : IToDoFactory
    {
        private readonly ICategoryService _categoryService;

        public ToDoFactory(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ToDoModel CreateToDoModel()
        {
            return new ToDoModel
            {
                DueDate = DateTime.Today,
                StatusId = (int)ToDoStatus.Todo
            };
        }

        public ToDoListViewModel CreateToDoListViewModel()
        {
            return new ToDoListViewModel
            {
                AvailableStatuses = GetStatusDropdownItems(),
                AvailableCategories = GetCategoryDropdownItems()
            };
        }

        public ToDoCreateEditViewModel CreateToDoCreateEditViewModel()
        {
            return new ToDoCreateEditViewModel
            {
                ToDo = CreateToDoModel(),
                Categories = GetCategoryDropdownItems(),
                Statuses = GetStatusDropdownItems()
            };
        }

        public ToDo CreateToDo(ToDoModel model)
        {
            return new ToDo
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                CategoryId = model.CategoryId,
                StatusId = model.StatusId
            };
        }

        public List<SelectListItem> GetCategoryDropdownItems()
        {
            return _categoryService.GetAllCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId,
                    Text = c.CategoryName
                }).ToList();
        }

        public List<SelectListItem> GetStatusDropdownItems()
        {
            return System.Enum.GetValues(typeof(ToDoStatus))
                .Cast<ToDoStatus>()
                .Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.ToString()
                }).ToList();
        }
    }
}