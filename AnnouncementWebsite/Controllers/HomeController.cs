using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Repositories;
using AnnouncementWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            AnnouncementListViewModel announcementListViewModel = new AnnouncementListViewModel();
            announcementListViewModel.Categories = _categoryRepository.AllCategories;
            return View(announcementListViewModel);
        }

    }
}
