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
        public async Task<ActionResult> Post([FromBody] Contact contact)
        {
            if (contact == null)
            {
                throw new Exception("Erro ao tentar criar um novo contato");
            }
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetContact", new { id = contact.ContactId }, contact);
        }

        // PUT api/<UsersControllers>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Contact contact)
        {
            if (id != contact.ContactId)
            {
                throw new Exception("Id informado está invállido");
            }

            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<UsersControllers>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> Delete(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);

            if (contact == null)
            {
                throw new Exception($"Erro ao tentar deletar o contato com o id {id}");
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return contact;
        }
    }
}
