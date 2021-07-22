using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contatos.Models
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            Contacts = new Collection<Contact>();
        }
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        
    }
}
