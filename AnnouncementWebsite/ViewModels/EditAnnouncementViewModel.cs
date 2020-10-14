using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace AnnouncementWebsite.ViewModels
{
    public class EditAnnouncementViewModel
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
       
        public string Description { get; set; }
        public string DateAdded { get; set; }
    }
}
