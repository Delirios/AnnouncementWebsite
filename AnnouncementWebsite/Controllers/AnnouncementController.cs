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

        public IActionResult AnnouncementList()
        {
            AnnouncementListViewModel announcementListViewModel = new AnnouncementListViewModel();
            AnnouncementCardViewModel announcementCardViewModel = new AnnouncementCardViewModel();
            announcementListViewModel.Announcements = _announcementRepository.AllAnnouncements;
            announcementListViewModel.CurrentCutegory = "Other";
            return View(announcementListViewModel);
        }
    }
}
