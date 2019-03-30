using Repository.Models;
using Repository.Repositories;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
