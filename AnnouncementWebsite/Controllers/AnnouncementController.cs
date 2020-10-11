using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;
using AnnouncementWebsite.Repositories;
using AnnouncementWebsite.Services;
using AnnouncementWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnnouncementWebsite.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly UserManager<AplicationUser> _userManager;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly AnnouncementControllerService _announcementControllerService;
        private readonly AnnouncementContext _announcementContext;

        public AnnouncementController(IAnnouncementRepository announcementRepository, ICategoryRepository categoryRepository,
            UserManager<AplicationUser> userManager,AnnouncementControllerService announcementControllerService,
            AnnouncementContext announcementContext)
        {
            _announcementContext = announcementContext;
            _announcementControllerService = announcementControllerService;
            _userManager = userManager;
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

        public ViewResult AddAnnouncement()
        {
            AnnouncementListViewModel announcementListViewModel = new AnnouncementListViewModel();
            announcementListViewModel.Categories = _categoryRepository.AllCategories;
            return View(announcementListViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<ViewResult> AddAnnouncement(Announcement announcement, List<IFormFile> files)
        {
            var userId = _userManager.GetUserId(User);
            announcement.DateAdded = DateTime.Today;
            announcement.AplicationUserId = userId;
            
            _announcementContext.Announcements.Add(announcement);
            _announcementContext.SaveChanges();
            var imageNames = await _announcementControllerService.UploadImages(files, userId);
            
            
            foreach (var imageName in imageNames)
            {
                AnnouncementImage announcementImage = new AnnouncementImage();
                Image image = new Image();
                image.Name = imageName;
                _announcementContext.Images.Add(image);
                _announcementContext.SaveChanges();
                announcementImage.AnnouncementId = announcement.AnnouncementId;
                announcementImage.Image = image;
                _announcementContext.AnnouncementImages.Add(announcementImage);
            }

            _announcementContext.SaveChanges();
            return View();
        }
    }
}
