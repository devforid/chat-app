using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;
using Shared.DTOs;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {

        UserServices _userServices = new UserServices();
        ChatServices chatServices = new ChatServices();

        //[HttpGet("{id}")]
        [HttpPost("getuserbyid")]
        public async Task<IActionResult> GetUserById(GetUser getUser)
        {
            try
            {
                var user = await _userServices.Get(getUser.Id);
                if(user == null)
                {
                    return Ok(new UserResponse()
                    {
                        StatusCode = 1,
                        isSuccess = false,
                    });
                }
                return Ok(new UserResponse()
                {
                    StatusCode = 0,
                    isSuccess = true,
                    Results =  user
                });
            }
            catch
            {
                return Ok(new UserResponse()
                {
                    StatusCode = 1,
                    isSuccess = false,
                });
            }
        }
        //[HttpPost]
        [HttpPost("Signup")]
        public async Task<IActionResult>  Insert([FromBody] Users user)
        {
            var response = await _userServices.InsertUser(user);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users user)
        {
            var response = _userServices.LoginUser(user);
            return Ok(response);
        }

        //[HttpGet]
        [HttpPost("getloggedinusers")]
        public async Task<IActionResult> GetLoggedInUsers()
        {
            try
            {
                var users = _userServices.GetLoggedInUsers();
                if (users != null)
                {
                    return Ok( new UserResponse()
                    {
                        StatusCode = 0,
                        Results = users,
                        isSuccess = true
                    });
                }
                return Ok( new UserResponse()
                {
                    StatusCode = 1,
                    isSuccess = false,
                });
            }
            catch
            {
                return Ok( new UserResponse()
                {
                    StatusCode = 1,
                    isSuccess = false,
                });
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