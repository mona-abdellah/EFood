using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Customer
{
    public class CreateORUpdateCustomerDTO
    {
        public Guid Id { get; set; }
        [MinLength(3)]
        public string FName { get; set; }
        [MinLength(3)]
        public string Lname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
