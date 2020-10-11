using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;
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

        public IActionResult AnnouncementList(string category)
        {
            IEnumerable<Announcement> announcements;
            Category currentCategory;

            announcements = _announcementRepository.AllAnnouncements.Where(a => a.Category.CategoryName == category)
                .OrderBy(a => a.AnnouncementId);

            currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category);


            return View(new AnnouncementListViewModel
            {
                Announcements = announcements,
                CurrentCategory = currentCategory
            });
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
