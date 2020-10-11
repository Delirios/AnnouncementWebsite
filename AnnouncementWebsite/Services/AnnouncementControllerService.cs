using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace AnnouncementWebsite.Services
{
    public class AnnouncementControllerService
    {
        public async Task<List<string>> UploadImages(List<IFormFile> files, string userId)
        {
            List<string> fileNames = new List<string>();
            if (files != null)
            {
                var UniqueFolderName = userId;

                string pathFolderString = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "announcement", UniqueFolderName);

                if (!Directory.Exists(pathFolderString))
                {
                    Directory.CreateDirectory(pathFolderString);
                }
                else
                {
                    foreach (var file in Directory.GetFiles(pathFolderString))
                    {
                        File.Delete(file);
                    }
                }

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
                        var filepath = Path.Combine(pathFolderString, newFileName);

                        var ImageName = Path.Combine(UniqueFolderName, newFileName);

                        fileNames.Add(ImageName);
                        using (FileStream fs = File.Create(filepath))
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
