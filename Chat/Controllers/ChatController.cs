using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Shared.DTOs;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        ChatServices chatServices = new ChatServices();
        //[HttpGet("{id}")]
        [HttpPost("getmessages")]
        public async Task<IActionResult> GetMessages(Message messageFilter)
        {
            var response = await chatServices.GetMessages(messageFilter);
            return Ok(response);
        }

        [HttpPost("GetPresignedUrl")]
        public async Task<IActionResult> GetPresignedUrl(PresignedUrl presignedUrl)
        {
            var response = chatServices.GetPresignedUrl(presignedUrl);
            return Ok(response);
        }


        [HttpPost("uploadFileInAwsS3")]
        public async Task<IActionResult> uploadFileInAwsS3(UploadFileAWSS3 uploadFileAWSS3)
        {
            var response = chatServices.uploadFileInAwsS3(uploadFileAWSS3);
            return Ok(response);
        }

        [HttpPost("getDownloadUrlAwsS3")]
        public async Task<IActionResult> getDownloadUrlAwsS3(DownloadFile downloadFile)
        {
            var response = chatServices.getDownloadUrlAwsS3(downloadFile);
            return Ok(response);
        }

        [HttpPost("downloadChatMessages")]
        public async Task<IActionResult> downloadChatMessages(DownloadChatMessage downloadChatMessage)
        {
            var response = chatServices.downloadChatMessages(downloadChatMessage);
            return Ok(response);
        }
    }
}