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
        public Task  UploadFileBlobAsync(string fileName, Stream content, string contentType);
        public Task DeleteFileBlobAsync(string fileName);
    }
}
