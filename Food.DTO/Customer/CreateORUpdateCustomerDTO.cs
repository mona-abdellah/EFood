using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Customer
{
    public class CreateORUpdateCustomerDTO
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string Lname { get; set; }

        public string Email { get; set; }
    }
}
