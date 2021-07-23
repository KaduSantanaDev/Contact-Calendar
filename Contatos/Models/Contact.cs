using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contatos.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Erro, o nome tem {1} caractéres mínimos")]
        public string Name { get; set; }
        [Required]      
        public long Number { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
