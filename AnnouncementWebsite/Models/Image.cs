using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebsite.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }

        public List<AnnouncementImage> AnnouncementImages { get; set; }

        public Image()
        {
            AnnouncementImages = new List<AnnouncementImage>();
        }
    }
}
