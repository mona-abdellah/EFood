using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Customer
{
    public class AdminDTO
    {
        public string Username { get; set; }
        [MinLength(3,ErrorMessage ="Minimum Length is 3")]
        public string FName { get; set; }
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public string Lname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
