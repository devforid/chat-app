using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shared.DTOs
{
    public class UserResponse
    {
        public int StatusCode { get; set; }
        public bool isSuccess { get; set; }
        public dynamic Results { get; set; }
    }
}
