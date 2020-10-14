using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace AnnouncementWebsite.Services
{
    public class AnnouncementControllerService
    {
        public async Task DeleteImages(Announcement announcement)
        {
            string folderName = "";
            var path = announcement.AnnouncementImages.Where(a => a.AnnouncementId == announcement.AnnouncementId)
                .Select(a=>a.Image.Name);
            foreach (var item in path)
            {
                folderName = Path.GetDirectoryName(item);
            }

            folderName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "announcement", folderName);

            if (Directory.Exists(folderName))
            {
                Directory.Delete(folderName, true);
            }
        }

        public async Task<List<string>> UploadImages(List<IFormFile> files, string userId)
        {
            List<string> fileNames = new List<string>();
            if (files != null)
            {
                var uniqueFolderName = userId;

                var uniqueAnnouncementFolderName = Convert.ToString(Guid.NewGuid());

                string pathFolderString = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "announcement", uniqueFolderName);
                string fullPath = Path.Combine(pathFolderString, uniqueAnnouncementFolderName);

                if (!Directory.Exists(pathFolderString))
                {
                    Directory.CreateDirectory(pathFolderString);
                }

                Directory.CreateDirectory(fullPath);

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(file.FileName);

                        //Assigning Unique Filename (Guid)
                        var UniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);

                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(UniqueFileName, fileExtension);

                        // Combines two strings into a path.
                        var filePath = Path.Combine(fullPath, newFileName);

                        var ImageName = Path.Combine(uniqueFolderName,uniqueAnnouncementFolderName, newFileName);

                        fileNames.Add(ImageName);
                        using (FileStream fs = File.Create(filePath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                    }
                }

            }

            return fileNames;
        }
    }
}
