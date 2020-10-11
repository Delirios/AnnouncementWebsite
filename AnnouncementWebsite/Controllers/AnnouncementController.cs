using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Repositories;
using AnnouncementWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementWebsite.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AnnouncementController(IAnnouncementRepository announcementRepository, ICategoryRepository categoryRepository)
        {
            _announcementRepository = announcementRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult AnnouncementList()
        {
            AnnouncementListViewModel announcementListViewModel = new AnnouncementListViewModel();
            announcementListViewModel.Announcements = _announcementRepository.AllAnnouncements;
            announcementListViewModel.Categories = _categoryRepository.AllCategories;
            return View(announcementListViewModel);
        }

        public IActionResult AnnouncementDetails(int id)
        {
            var announcement = _announcementRepository.GetAnnouncementById(id);
            if (announcement == null)
                return NotFound();
            return View(announcement);
        }
    }
}
