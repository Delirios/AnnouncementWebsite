﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;

namespace AnnouncementWebsite.ViewModels
{
    public class AnnouncementListViewModel
    {
        public IEnumerable<Announcement> Announcements { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public Category CurrentCategory { get; set; }
        public Announcement Announcement { get; set; }


    }
}
