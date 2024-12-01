using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Category
{
    public class CreateORupdateCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
