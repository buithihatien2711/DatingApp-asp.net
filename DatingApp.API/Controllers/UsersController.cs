using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // AllowAnonymous: ko cần login vẫn có thể sử dụng phương thức
        // Nếu class ko có attribute Authorize thì mặc định các method là AllowAnonymous
        // Nếu class có attribute Athorize thì tất cả method là Authorize, muốn method là AllowAnonymous thì phải thêm attribute AllowAnonymous
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_context.Users);
        }

        // Authorize: phải pass Authentication(verifies the identity of a user or service) và Authorization (determines their access rights) mới vào được
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<User > Get(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}