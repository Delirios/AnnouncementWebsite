using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;
using AnnouncementWebsite.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace AnnouncementWebsite.Services
{
    public class AnnouncementControllerService
    {
        private readonly IBlobRepository _blobRepository;

        public AnnouncementControllerService(IBlobRepository blobRepository)
        {
            _blobRepository = blobRepository;
        }

        public async Task DeleteImages(Announcement announcement)
        {
            string folderName = "";
            var path = announcement.AnnouncementImages.Where(a => a.AnnouncementId == announcement.AnnouncementId)
                .Select(a => a.Image.Name);
            foreach (var item in path)
            {
                folderName = item;
            }

            await _blobRepository.DeleteFileBlobAsync(folderName);
        }

        public async Task<string> UploadImagesToAzure(IFormFile file, string userId)
        {
            string imageName = "";
            if (file != null)
            {
                var uniqueFolderName = userId;

                var uniqueAnnouncementFolderName = Convert.ToString(Guid.NewGuid());
                //Getting FileName
                var fileName = Path.GetFileName(file.FileName);
                //Assigning Unique Filename (Guid)
                var UniqueFileName = Convert.ToString(Guid.NewGuid());
                //Getting file Extension
                var fileExtension = Path.GetExtension(fileName);
                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(UniqueFileName, fileExtension);

                imageName = Path.Combine(uniqueFolderName, uniqueAnnouncementFolderName, newFileName);

                await _blobRepository.UploadFileBlobAsync(imageName, file.OpenReadStream(), file.ContentType);

            }
            return imageName;
        }
    }
}
