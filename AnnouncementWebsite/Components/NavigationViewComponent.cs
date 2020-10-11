using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Repositories;
using AnnouncementWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementWebsite.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public NavigationViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AnnouncementListViewModel announcementListViewModel = new AnnouncementListViewModel();
            announcementListViewModel.Categories = _categoryRepository.AllCategories;
            return await Task.FromResult((IViewComponentResult) View("_Navigation", announcementListViewModel));
        }
    }
}
