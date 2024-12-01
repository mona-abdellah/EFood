using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Shared
{
    public class EntityPagenated<T>
    {
        public List<T> Data { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);
    }
}
