using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_MongoDB.Models;
using ASP.NET_MongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MongoDB.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly UserService _userServices;

        public UserController(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
            _userServices = userService;
        }

        [HttpPost]
        public String Index()
        {
            var myVariable = _configuration["MY_VARIABLE"];
            Console.WriteLine(myVariable);
            _userServices.CreateUser();
            return myVariable;
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return _userServices.GetUsers();
        }

        [HttpPost("{id}")]
        public User GetUser(int id)
        {
            return _userServices.GetUser(id);
        }

        [HttpPut]
        public String UpdateUser(User user)
        {
            _userServices.UpdateUser(user.Id, user);
            return "Updated";
        }

        [HttpDelete]
        public String DeleteUser(int id)
        {
            _userServices.DeleteUser(id);
            return "Deleted";
        }
    }
}

