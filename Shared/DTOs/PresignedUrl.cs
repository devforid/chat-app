using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class PresignedUrl
    {
        public string FileId { get; set; }
        public string FileExtention { get; set; }
        public string FileKey { get; set; }
        public string Url { get; set; }
    }
}
