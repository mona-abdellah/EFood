using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Models
{
    public  class BaseEntity<T>
    {
        public Guid Id { get; set; }
        public DateTime? Create { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
