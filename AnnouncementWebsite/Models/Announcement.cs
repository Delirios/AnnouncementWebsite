using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AnnouncementWebsite.Models
{
    public class Announcement
    {

        public int AnnouncementId { get; set; }
        //[Required(ErrorMessage = "Please enter title")]
        //[StringLength(50)]
        public string Title { get; set; }
        //[Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }
        [Display(Name = "Date Added")]
        public string DateAdded { get; set; }
        [Display(Name = "Category")]
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
