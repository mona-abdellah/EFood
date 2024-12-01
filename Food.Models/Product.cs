using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Models
{
    public class Product:BaseEntity<Guid>
    {
        [MinLength(3)]
        public string name { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string Image { get; set; }

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
    }
}
