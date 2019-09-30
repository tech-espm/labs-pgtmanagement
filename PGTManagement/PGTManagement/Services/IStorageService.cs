using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGTManagement.Services
{
    public interface IStorageService
    {
        Task<string> UploadFile(IFormFile file, string containerName, string fileName);
        Task<bool> RemoveFile(string containerName, string fileName);
        Task<BlobDownloadModel> GetByBlobName(string containerName, string fileName);
        Task<bool> AddMessage(string Queue, string req);
    }
}
