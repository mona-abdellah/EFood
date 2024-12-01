using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Shared
{
    public class ResultView<T>
    {
        public T? Entity { get; set; }
        public bool? ISSuccess { get; set; }
        public string? Message {  get; set; }
    }
}
