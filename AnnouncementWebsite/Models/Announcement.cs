using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AnnouncementWebsite.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string AplicationUserId { get; set; }

        public AplicationUser AplicationUser { get; set; }
        
        public List<AnnouncementImage> AnnouncementImages { get; set; }

        public Announcement()
        {
            AnnouncementImages = new List<AnnouncementImage>();
        }
    }
}
