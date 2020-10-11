using System;
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
                return _announcementContext.Announcements.Include(a=>a.Category)
                    .Include(a => a.AnnouncementImages)
                    .ThenInclude(a => a.Image);
            }
        }

        public void CreateAnnouncement(Announcement announcement)
        {
            throw new NotImplementedException();
        }

        public Announcement GetAnnouncementById(int announcementId)
        {
            return _announcementContext.Announcements.FirstOrDefault(a => a.AnnouncementId == announcementId);
        }


    }
}
