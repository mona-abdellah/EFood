using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Product
{
    public class CreateORUpdateProductDTO
    {
        public Guid Id { get; set; }
        [MinLength(3,ErrorMessage ="Minimum Length is 3")]
        public string name { get; set; }
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public decimal price { get; set; }
        public int stock { get; set; }
        public IFormFile Image { get; set; }
    }
}
