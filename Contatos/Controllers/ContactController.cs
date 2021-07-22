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
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext contexto)
        {
            _context = contexto;
        }

        // GET: api/<UsersControllers>
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> Get()
        {
            return _context.Contacts.ToList();
        }

        // GET api/<UsersControllers>/5
        [HttpGet("{id}", Name = "GetContact")]
        public ActionResult<Contact> Get(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
            {
                return NotFound("Contact not found");
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
