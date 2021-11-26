using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Repository.Repositories;

namespace Services.Services
{
    public class ChatServices
    {
        ChatRepository chatRepository;
        public ChatServices()
        {
            chatRepository = new ChatRepository();
        }

        public async Task<UserResponse> GetMessages(Message messageFilter)
        {
            try
            {
                List<Message> messages = chatRepository.GetMessage(messageFilter);
                if (messages == null)
                {
                    return new UserResponse()
                    {
                        StatusCode = 1,
                        isSuccess = false,
                    };
                }
                return new UserResponse()
                {
                    StatusCode = 0,
                    isSuccess = true,
                    Results = messages
                };
            }
            catch (Exception ex)
            {
                var e = ex;
                return new UserResponse()
                {
                    StatusCode = 1,
                    isSuccess = false,
                };
            }
        }

        public PresignedUrl GetPresignedUrl(PresignedUrl fileInfo)
        {
            try
            {
                PresignedUrl presignedUrl = chatRepository.GetPresignedUrl(fileInfo);
                Task task = Task.Run(() => chatRepository.storePresignedUrl(presignedUrl));
                return presignedUrl;

            }
            catch (Exception ex)
            {
                var ee = ex;
                return new PresignedUrl();
            }

        }
        public bool uploadFileInAwsS3(UploadFileAWSS3 uploadFileAWSS3)
        {
            try
            {
                return chatRepository.uploadFileInAwsS3(uploadFileAWSS3);

            }
            catch (Exception ex)
            {
                var ee = ex;
                return false;
            }
        }

        public DownloadFile getDownloadUrlAwsS3(DownloadFile downloadFile)
        {
            try
            {
                return chatRepository.getDownloadUrlAwsS3(downloadFile);
            }catch(Exception e)
            {
                var ex = e;
                return new DownloadFile();
            }
        }

        public DownloadChatMessage downloadChatMessages(DownloadChatMessage downloadChatMessage)
        {
            try
            {
                Message message = new Message()
                {
                    CreatorId = downloadChatMessage.CreatorId
                };

                var messages = chatRepository.GetMessage(message);
                var jsonMessages = JsonConvert.SerializeObject(messages);
                var fileDirectory = "G:\\messages.txt";
                using (StreamWriter file = File.CreateText(@fileDirectory))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, jsonMessages);
                }

                PresignedUrl fileInfo = new PresignedUrl()
                {
                    FileId = Guid.NewGuid().ToString(),
                    FileExtention = "txt"
                };

                var presignedUrl = chatRepository.GetPresignedUrl(fileInfo);
                Task task = Task.Run(() => chatRepository.storePresignedUrl(presignedUrl));

                UploadFileAWSS3 uploadFileAWSS3 = new UploadFileAWSS3()
                {
                    FilePath = fileDirectory,
                    FileUrl = presignedUrl.Url
                };
                Task uploadTask = Task.Run(() => chatRepository.uploadFileInAwsS3(uploadFileAWSS3));

                DownloadFile downloadFile = new DownloadFile()
                {
                    FileKey = presignedUrl.FileKey
                };

                var downloadFileInfo = chatRepository.getDownloadUrlAwsS3(downloadFile);
                
                return new DownloadChatMessage()
                {
                    CreatorId = downloadChatMessage.CreatorId,
                    DownloadUrl = downloadFileInfo.DownloadUrl
                };

            }
            catch(Exception e)
            {
                return new DownloadChatMessage();
            }
        }
    }
}
