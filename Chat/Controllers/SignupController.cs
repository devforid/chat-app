using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Services.Interfaces;
using Services.Services;
using Shared.DTOs;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SignupController : ControllerBase
    {

        UserServices _userServices = new UserServices();

        [HttpGet("{id}")]
        public UserResponse Get(string id)
        {
            try
            {
                var user = _userServices.Get(id);
                if(user == null)
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
                    Results =  user
                };
            }
            catch
            {
                return new UserResponse()
                {
                    StatusCode = 1,
                    isSuccess = false,
                };
            }
        }
        [HttpPost]
        public SignupResponse Insert(Users user)
        {
            return _userServices.InsertUser(user);
        }

        [HttpGet]
        public UserResponse Get()
        {
            try
            {
                var users = _userServices.GetLoggedInUsers();
                if (users != null)
                {
                    return new UserResponse()
                    {
                        StatusCode = 0,
                        Results = users,
                        isSuccess = true
                    };
                }
                return new UserResponse()
                {
                    StatusCode = 1,
                    isSuccess = false,
                };
            }
            catch
            {
                return new UserResponse()
                {
                    StatusCode = 1,
                    isSuccess = false,
                };
            }

        }

        //[HttpPost]
        //public SignupResponse Update(Users user)
        //{
        //    return _userServices.InsertUser(user);
        //}

        //[HttpPost]
        //public SignupResponse Signup(UserSignUp user)
        //{
        //    return new SignupResponse();
        //}


        // GET api/values
        //  [HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }

}