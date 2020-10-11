using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;
using AnnouncementWebsite.ViewModels;

namespace AnnouncementWebsite.Repositories
{
    public interface IAnnouncementRepository
    {

        public IEnumerable<Announcement> AllAnnouncements { get; }

        public Announcement GetAnnouncementById(int announcementId);

    }
}
