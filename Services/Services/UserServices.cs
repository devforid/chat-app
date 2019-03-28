﻿using Repository.Models;
using Repository.Repositories;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
   public class UserServices
    {
        UserSignuRipository _userSignuRipository;
        public UserServices()
        {
            _userSignuRipository = new UserSignuRipository();
        }

        public dynamic Get(string id)
        {
            var user = _userSignuRipository.Get(id);
            return modifiedUserInfo(user);
        } 
        public SignupResponse InsertUser(Users user)
        {
            string hashedPassword = ComputeSha256Hash(user.Password);
            user.Password = hashedPassword;
            user.isLoggedin = true;
            try
            {
               var userInfo = _userSignuRipository.Insert(user);
                if(userInfo != null)
                {
                    return new SignupResponse()
                    {
                        isSignupSuccess = true,
                        Username = user.Username
                    };
                }
                return new SignupResponse()
                {
                    isSignupSuccess = false,
                    Username = user.Username
                };
            }
            catch
            {
                return new SignupResponse()
                {
                    isSignupSuccess = false,
                    Username = user.Username
                };
            }
        }
        public SignupResponse Updateuser(Users user)
        {
            try
            {
                var userInfo = _userSignuRipository.Update(user);
                if (userInfo != null)
                {
                    return new SignupResponse()
                    {
                        isSignupSuccess = true,
                        Username = user.Username
                    };
                }
                return new SignupResponse()
                {
                    isSignupSuccess = false,
                    Username = user.Username
                };
            }
            catch
            {
                return new SignupResponse()
                {
                    isSignupSuccess = false,
                    Username = user.Username
                };
            }

        }

        public List<dynamic> GetLoggedInUsers()
        {
            List<Users> userList = _userSignuRipository.GetLoggedInUsers();
            List<dynamic> modifieduserList = new List<dynamic>();
            var totalUser = userList.Count;
            for (int i=0;i<totalUser; i++)
            {
                var modified = modifiedUserInfo(userList[i]);
                modifieduserList.Add(modified);
            }
            return modifieduserList;
        }

        static dynamic modifiedUserInfo(Users user)
        {
            dynamic userInfo = new ExpandoObject();
            userInfo.ItemId = user.Id;
            userInfo.Username = user.Username;
            userInfo.isLoggedin = user.isLoggedin;
            return userInfo;
        }

        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
