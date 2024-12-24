using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Category
{
    public class CreateORupdateCategoryDTO
    {
        public Guid Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageData { get; set; }
    }
}
