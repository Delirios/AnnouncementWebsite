using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementWebsite.Repositories
{
    public class AnnouncementRepository :IAnnouncementRepository
    {
        private readonly AnnouncementContext _announcementContext;

        public AnnouncementRepository(AnnouncementContext announcementContext)
        {
            _announcementContext = announcementContext;
        }

        public IEnumerable<Announcement> AllAnnouncements
        {
            get
            {
                return  _announcementContext.Announcements.Include(a => a.Category)
                    .Include(a => a.AnnouncementImages)
                    .ThenInclude(a => a.Image);
            }
        }

        public IEnumerable<Announcement> SimilarAnnouncements(Announcement announcement)
        {
            var similarWords = announcement.Title.ToLower().Split(" " , StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
            similarWords.AddRange(announcement.Description.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries).Distinct().Where(c=>c.Length>1).ToList());

            List<Announcement> announcementsList= new List<Announcement>();
            foreach (var item in similarWords.Distinct())
            {
                var result = _announcementContext.Announcements.Include(a => a.Category)
                    .Include(a => a.AnnouncementImages)
                    .ThenInclude(a => a.Image).Where(a => (a.Description.Contains(item) | a.Title.Contains(item)) & a.AnnouncementId != announcement.AnnouncementId);
                announcementsList.AddRange(result);
            }

            return announcementsList.Distinct();
        }
        public IEnumerable<Announcement> SimilarAnnouncements(string SearchString, string categoryName)
        {
            var similarWords = SearchString.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            List<Announcement> announcementsList = new List<Announcement>();
            if (categoryName == null)
            {
                foreach (var item in similarWords.Distinct())
                {
                    var result = _announcementContext.Announcements.Include(a => a.Category)
                        .Include(a => a.AnnouncementImages)
                        .ThenInclude(a => a.Image).Where(a => a.Title.Contains(item) | a.Description.Contains(item));
                    announcementsList.AddRange(result);
                }
            }
            else
            {
                foreach (var item in similarWords.Distinct())
                {
                    var result = _announcementContext.Announcements.Include(a => a.Category)
                        .Include(a => a.AnnouncementImages)
                        .ThenInclude(a => a.Image)
                        .Where(a => a.Title.Contains(item) & a.Category.CategoryName == categoryName);
                    announcementsList.AddRange(result);
                }
            }

            return announcementsList;
        }



        public async Task<Announcement> GetAnnouncementById(int announcementId)
        { 
            return await _announcementContext.Announcements.Include(a=>a.AnnouncementImages)
                .ThenInclude(a=>a.Image).FirstOrDefaultAsync(a => a.AnnouncementId == announcementId);
        }


    }
}
