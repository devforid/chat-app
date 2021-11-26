using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUsers
    {
        UserResponse GetUserById(GetUser getUser);
        SignupResponse Insert(Users user);
        SignupResponse Login(Users user);
        UserResponse GetLoggedInUsers();
    }
}
