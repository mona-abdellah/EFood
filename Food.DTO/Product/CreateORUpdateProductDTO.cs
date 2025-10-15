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
        public CreateORUpdateProductDTO()
        {
            IsDeleted = false;
        }
        public Guid Id { get; set; }
        [MinLength(3,ErrorMessage ="Minimum Length is 3")]
        public string name { get; set; }      
        public decimal price { get; set; }
        public int stock { get; set; }
		public Guid? CategoryId { get; set; }
		public string? Image { get; set; }
        public IFormFile? ImageData { get; set; }
        public bool? IsDeleted { get; set; }
	}
}
