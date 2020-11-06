using AnnouncementWebsite.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebsite.Services
{
    public interface IAnnouncementControllerService
    {
        Task DeleteImages(Announcement announcement);
        Task UploadImagesToAzure(IFormFile file, string imageName);
        Task UploadImagesToAWS(IFormFile file, string imageName);

        Task<string> UploadImages(IFormFile file, string userId);
    }
}
