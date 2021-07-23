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
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext contexto)
        {
            _context = contexto;
        }

        // GET: api/<UsersControllers>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return await _context.Contacts.ToListAsync();
        }

        // GET api/<UsersControllers>/5
        [HttpGet("{id}", Name = "GetContact")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            

            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);
            if (contact == null)
            {
                throw new Exception("Id informado está inválido");
            }
            else
            {
                return contact;
            }
        }

        // POST api/<UsersControllers>
        [HttpPost]
        public ActionResult Post([FromBody] Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetContact", new { id = contact.ContactId }, contact);
        }

        // PUT api/<UsersControllers>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return BadRequest("Id not found");
            }

            _context.Entry(contact).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/<UsersControllers>/5
        [HttpDelete("{id}")]
        public ActionResult<Contact> Delete(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id);

            if (contact == null)
            {
                return NotFound("User not found");
            }

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return contact;
        }
    }
}
