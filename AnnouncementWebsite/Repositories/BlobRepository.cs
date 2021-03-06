﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobInfo = AnnouncementWebsite.Models.BlobInfo;

namespace AnnouncementWebsite.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        //private readonly BlobServiceClient _blobServiceClient;
        //private readonly string containerName = "announcement";
        string AWS_accessKey = "accessKey";
        string AWS_secretKey = "secretKey";
        private readonly string bucketName = "myannouncement";

        private static IAmazonS3 s3Client;

        public BlobRepository()
        {
            //_blobServiceClient = blobServiceClient;
            s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, bucketRegion);
        }

        #region AZURE
        //public async Task UploadFileBlobAsync(string fileName, Stream content, string contentType)
        //{
        //    var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        //    var blobClient = containerClient.GetBlobClient(fileName);
        //    await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
        //}
        //public async Task DeleteFileBlobAsync(string fileName)
        //{
        //    var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        //    var blobClient = containerClient.GetBlobClient(fileName);
        //    await blobClient.DeleteAsync();
        //}
        #endregion

        #region AWS
        public async Task UploadFileS3Async(string fileName, Stream content, string contentType)
        {

            using (var newMemoryStream = new MemoryStream())
            {
                content.CopyTo(newMemoryStream);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = fileName,
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.PublicRead
                };

                var fileTransferUtility = new TransferUtility(s3Client);
                await fileTransferUtility.UploadAsync(uploadRequest);
            }

            //var uploadMultipartRequest = new TransferUtilityUploadRequest
            //{
            //    BucketName = "defaultBucket",
            //    Key = "key",
            //    InputStream = content,
            //    PartSize = partSize
            //};

            //FileStream fs = new FileStream(content.EndRead(), FileMode.Open);

            //var fileTransferUtility = new TransferUtility(s3Client);
            //using (var fileToUpload = new FileStream(content, FileMode.Open, FileAccess.Read))
            //{
            //    await fileTransferUtility.UploadAsync(fileToUpload, bucketName, fileName);
            //}
        }
        public async Task DeleteFileS3Async(string fileName)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };
               
                await s3Client.DeleteObjectAsync(deleteObjectRequest);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
            }
        }
        #endregion
    }




}
