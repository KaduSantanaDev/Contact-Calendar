using Contatos.Context;
using Contatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersControllers(AppDbContext contexto)
        {
            _context = contexto;
        }

        // GET: api/<UsersControllers>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _context.Users.ToList();
        }

        // GET api/<UsersControllers>/5
        [HttpGet("{id}",Name = "GetUser")]
        public ActionResult<User> Get(int id)
        {
            var user = _context.Users.FirstOrDefault(p => p.UserId == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return user;
            }
        }

        // POST api/<UsersControllers>
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetUser", new { id = user.UserId }, user);
        }

        // PUT api/<UsersControllers>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("Id not found");
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/<UsersControllers>/5
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (User == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }
    }
}
