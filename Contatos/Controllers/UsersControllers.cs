using Contatos.Context;
using Contatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("contacts")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserContacts()
        {
            return await _context.Users.Include(u => u.Contacts).ToListAsync();
        }

        // GET api/<UsersControllers>/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<User>> Get(int id)
        {          
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == id);
            if (user == null)
            {
                throw new Exception("Id informado está inválido");
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

            if (user == null)
            {
                throw new Exception("Usuário inválido");
            }
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
                throw new Exception("Id informado está inválido");
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
                throw new Exception("Id informado está inválido");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }
    }
}
