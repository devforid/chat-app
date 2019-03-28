using Repository.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUsers
    {
        UserResponse Get(string id);
        SignupResponse Insert(Users user);
        SignupResponse Update(Users user);
    }
}
