using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using PGTManagement.Services;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;

namespace wExponencial.Service
{
    public class StorageService : IStorageService
    {
        private readonly CloudStorageAccount _cloudStorageAccount;

        public StorageService(string StorageConnectionString)
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(StorageConnectionString);
        }

        public async Task<bool> AddMessage(string Queue, string req)
        {
            try
            {
                // Create the queue client.
                CloudQueueClient queueClient = _cloudStorageAccount.CreateCloudQueueClient();

                // Retrieve a reference to a queue.
                CloudQueue queue = queueClient.GetQueueReference(Queue);

                // Create the queue if it doesn't already exist.
                await queue.CreateIfNotExistsAsync();

                // Create a message and add it to the queue.
                CloudQueueMessage message = new CloudQueueMessage(req);
                await queue.AddMessageAsync(message);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveFile(string containerName, string fileName)
        {
            try
            {
                CloudBlobClient blobClient = _cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                await container.CreateIfNotExistsAsync();

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                return await blockBlob.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BlobDownloadModel> GetByBlobName(string containerName, string blobName)
        {
            try
            {
                CloudBlobClient blobClient = _cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                await container.CreateIfNotExistsAsync();

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);


                var ms = new MemoryStream();
                await blockBlob.DownloadToStreamAsync(ms);

                var blobDownload = new BlobDownloadModel
                {
                    BlobStream = ms,
                    BlobFileName = blobName,
                    BlobLength = blockBlob.Properties.Length,
                    BlobContentType = blockBlob.Properties.ContentType
                };


                return blobDownload;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetImageUrl(string containerName, string blobName)
        {
            try
            {
                CloudBlobClient blobClient = _cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                await container.CreateIfNotExistsAsync();

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);


                SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(24);
                sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

                //Generate the shared access signature on the container, setting the constraints directly on the signature.
                string sasContainerToken = container.GetSharedAccessSignature(sasConstraints);

                //Return the URI string for the container, including the SAS token.
                //return container.Uri + sasContainerToken;

                return blockBlob.Uri + sasContainerToken;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UploadFile(IFormFile file, string containerName, string fileName)
        {
            try
            {

                CloudBlobClient blobClient = _cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                await container.CreateIfNotExistsAsync();

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                blockBlob.Properties.ContentType = "image/jpeg";

                using (var fileStream = file.OpenReadStream())
                {

                    await blockBlob.UploadFromStreamAsync(fileStream);
                }

                return blockBlob.Name;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
