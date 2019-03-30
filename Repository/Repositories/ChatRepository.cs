using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using MongoDB.Driver;
using Repository.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Repository.Repositories
{

   
    public class ChatRepository
    {
        private IMongoDatabase _db;
        //private AmazonS3Config amazonS3Config;
        private AmazonS3Client amazonS3Client;
        private readonly string awsS3AccessKey = "AKIAXJJI56KALITBRA3H";
        private readonly string awsS3SecretKey = "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";
        private readonly string awsS3BucketName = "stp-selise";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUCentral1;
        public ChatRepository()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            _db = client.GetDatabase("UserDB");
            var amazonConfig = new AmazonS3Config { RegionEndpoint = RegionEndpoint.GetBySystemName(bucketRegion.SystemName.ToString()) };
            amazonS3Client = new AmazonS3Client(awsS3AccessKey, awsS3SecretKey, amazonConfig);
        }

        public List<Message> GetMessage(Message messageFilter)
        {
            var messages = _db.GetCollection<Message>("Messages").Find(m => m.CreatorId == messageFilter.CreatorId).ToList();
            return messages;
        }

        public PresignedUrl GetPresignedUrl(PresignedUrl fileInfo)
        {
            GetPreSignedUrlRequest presignedUrlRequest = new GetPreSignedUrlRequest
            {
                Key = Guid.NewGuid() + "/" + fileInfo.FileId + "."+ fileInfo.FileExtention,
                BucketName = awsS3BucketName,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddDays(5)
            };

            var uploadUrl = amazonS3Client.GetPreSignedURL(presignedUrlRequest);
            PresignedUrl presignedUrl = new PresignedUrl()
            {
                FileId = fileInfo.FileId,
                FileKey = presignedUrlRequest.Key,
                Url = uploadUrl,
                FileExtention = fileInfo.FileExtention
            };
            return presignedUrl;
        }
        public bool uploadFileInAwsS3(UploadFileAWSS3 uploadFileAWSS3)
        {
            using (var uploadRequest = new HttpRequestMessage(HttpMethod.Put, uploadFileAWSS3.FileUrl))
            {
                using (var fileStream = File.OpenRead( uploadFileAWSS3.FilePath))
                {
                    uploadRequest.Content = new StreamContent(fileStream);

                    using (var httpClient = new HttpClient())
                    {
                        var response = httpClient.SendAsync(uploadRequest).GetAwaiter().GetResult();
                        if (response.IsSuccessStatusCode == true)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }

        public DownloadFile getDownloadUrlAwsS3(DownloadFile downloadFile)
        {
            var presignedUrlRequest = new GetPreSignedUrlRequest
            {
                Key = downloadFile.FileKey,
                BucketName = awsS3BucketName,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            var downloadUrl = amazonS3Client.GetPreSignedURL(presignedUrlRequest);
            DownloadFile downloadFileUrl = new DownloadFile()
            {
                FileKey = downloadFile.FileKey,
                DownloadUrl = downloadUrl
            };
            return downloadFileUrl;
        }

        public void storePresignedUrl(PresignedUrl presignedUrl)
        {
            try
            {
                UplodedFile file = new UplodedFile()
                {
                    FileId = presignedUrl.FileId,
                    FileKey = presignedUrl.FileKey,
                    _id = Guid.NewGuid().ToString()
                };
                _db.GetCollection<UplodedFile>("UplodedFiles").InsertOne(file);
            }catch(Exception e)
            {

            }

        }


    }
}
