using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase

    {
        private UsersDbContext _usersDbContext;

        public UsersController(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            try
            {
                var users = _usersDbContext.UserDetails.ToList();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "Users not found");
            }
        }

        [HttpPost("CreateUsers")]
        public IActionResult Create([FromBody]UserDetails details)
        {
            UserDetails Usersdt = new UserDetails();
            Usersdt.FullName = details.FullName;
            Usersdt.Course = details.Course;
            Usersdt.Phone = details.Phone;

            try
            {
                _usersDbContext.UserDetails.Add(Usersdt);
                _usersDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
            var users = _usersDbContext.UserDetails.ToList();
            return Ok(users);

        }

        [HttpPut("UpdateUsers")]
        public IActionResult Update([FromBody]UserDetails details)
        {
            try
            {
                var User = _usersDbContext.UserDetails.FirstOrDefault(x => x.Id == details.Id);
                if (User == null)
                {
                    return StatusCode(404, "Users not Found");
                }
                
                User.FullName = details.FullName;
                User.Course = details.Course;
                User.Phone = details.Phone;

                _usersDbContext.Entry(User).State = EntityState.Modified;
                _usersDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error Occurred");
            }
            var users = _usersDbContext.UserDetails.ToList();
            return Ok(users);
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete([FromRoute]int Id)
        {
            try
            {
                var user = _usersDbContext.UserDetails.FirstOrDefault(x => x.Id == Id);
                if (user == null)
                {
                    return StatusCode(404, "user not found");
                }
                _usersDbContext.Entry(user).State = EntityState.Deleted;
                _usersDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
           

          var users =  _usersDbContext.UserDetails.ToList();
            return Ok(users);
        }
    }
}
