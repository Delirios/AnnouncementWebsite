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

        public IActionResult AnnouncementList(string category,string SearchString)
        {
            IEnumerable<Announcement> announcements = new List<Announcement>();
            Category currentCategory = new Category();
            string categoryName = null;
            if (category != null & category !="All")
            {
                categoryName = category;
                if(SearchString !=null)
                {
                    announcements = _announcementRepository.SimilarAnnouncements(SearchString, categoryName).OrderBy(a=>a.AnnouncementId);
                }
                else
                {
                    announcements = _announcementRepository.AllAnnouncements
                        .Where(a => a.Category.CategoryName == category)
                        .OrderBy(a => a.AnnouncementId);
                }
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category);
                
                return View(new AnnouncementListViewModel
                {
                    Announcements = announcements,
                    CurrentCategory = currentCategory
                });
            }

            else
            {
                if (SearchString != null)
                {
                    announcements = _announcementRepository.SimilarAnnouncements(SearchString, categoryName).OrderBy(a => a.AnnouncementId);

                }
                currentCategory = new Category() { CategoryName = "All" };
                return View(new AnnouncementListViewModel
                {
                    Announcements = announcements,
                    CurrentCategory = currentCategory
                });

            }
        }

        public IActionResult AnnouncementDetails(int id)
        {
            var announcement = _announcementRepository.GetAnnouncementById(id);
            if (announcement == null)
                return NotFound();
            var similarAnnouncements = _announcementRepository.SimilarAnnouncements(announcement).Take(3);
            return View(new AnnouncementListViewModel
            {
                Announcements = similarAnnouncements,
                Announcement = announcement
            });
        }
        public async Task<IActionResult> EditAnnouncement(int id)
        {
            if (id != null)
            {
                var announcement = _announcementRepository.GetAnnouncementById(id);
                if (announcement == null)
                    return NotFound();
                var editAnnouncement = new EditAnnouncementViewModel()
                {
                    AnnouncementId = announcement.AnnouncementId,
                    Title = announcement.Title,
                    Description = announcement.Description
                };
                return View(editAnnouncement);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAnnouncement(EditAnnouncementViewModel editAnnouncement)
        {
            var announcement = _announcementRepository.GetAnnouncementById(editAnnouncement.AnnouncementId);

            announcement.Title = editAnnouncement.Title;
            announcement.Description = editAnnouncement.Description;
            announcement.DateAdded = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

            _announcementContext.Announcements.Update(announcement);
            await _announcementContext.SaveChangesAsync();
            return RedirectToAction("AnnouncementDetails" ,new {id = announcement.AnnouncementId});
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcement = _announcementRepository.GetAnnouncementById(id);
            if (announcement != null)
            {
                await _announcementControllerService.DeleteImages(announcement);
                _announcementContext.Announcements.Remove(announcement);
                await _announcementContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ViewResult AddAnnouncement()
        {
            AnnouncementListViewModel announcementListViewModel = new AnnouncementListViewModel();
            announcementListViewModel.Categories = _categoryRepository.AllCategories;
            return View(announcementListViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<RedirectToActionResult> AddAnnouncement(Announcement announcement, IFormFile file)
        {
            var userId = _userManager.GetUserId(User);
            announcement.DateAdded = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            announcement.AplicationUserId = userId;
            
            _announcementContext.Announcements.Add(announcement);
            _announcementContext.SaveChanges();
            
            var imageName = await _announcementControllerService.UploadImagesToAzure(file, userId);
            
            AnnouncementImage announcementImage = new AnnouncementImage();
            Image image = new Image();
            image.Name = imageName;
            _announcementContext.Images.Add(image);
            _announcementContext.SaveChanges();
            announcementImage.AnnouncementId = announcement.AnnouncementId;
            announcementImage.Image = image;
            _announcementContext.AnnouncementImages.Add(announcementImage);

            _announcementContext.SaveChanges();

            return RedirectToActionPermanent("Index", "Home");
        }

        public async Task<IActionResult> MyAnnouncementList()
        {
            var userId = _userManager.GetUserId(User);
            var myAnnouncement = _announcementRepository.AllAnnouncements.Where(a => a.AplicationUserId == userId);

            return View(new AnnouncementListViewModel
            {
                Announcements = myAnnouncement
            });
        }
       
    }
}
