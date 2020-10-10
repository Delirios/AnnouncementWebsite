using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebsite.Models
{
    public class AnnouncementImage
    {
        public int AnnouncementImageId { get; set; }
        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
