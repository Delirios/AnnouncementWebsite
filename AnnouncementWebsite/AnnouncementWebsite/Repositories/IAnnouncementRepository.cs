using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;

namespace AnnouncementWebsite.Repositories
{
    public interface IAnnouncementRepository
    {
        IEnumerable<Announcement> AllAnnouncements { get; }
        Announcement GetAnnouncementById { get; }
    }
}
