using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;

namespace AnnouncementWebsite.Repositories
{
    public interface IBlobRepository
    {
        //Task  UploadFileBlobAsync(string fileName, Stream content, string contentType);
        //Task DeleteFileBlobAsync(string fileName);
        Task UploadFileS3Async(string fileName, Stream content, string contentType);
        
        Task DeleteFileS3Async(string fileName);

    }
}
