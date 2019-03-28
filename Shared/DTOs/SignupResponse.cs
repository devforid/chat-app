using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class SignupResponse
    {
        public string Username { get; set; }
        public bool isSignupSuccess { get; set; }
    }
}
