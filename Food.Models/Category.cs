using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Models
{
    public class Category:BaseEntity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
