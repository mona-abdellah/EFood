using Food.Apllication.Contracts;
using Food.Context;
using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infrastructure
{
    public class OrderRepository:GenericRepository<Order,Guid>,IOrderRepositotory
    {
        private readonly FoodContext context;
        public OrderRepository(FoodContext _context):base(_context)
        {
            this.context = _context;
        }
    }
}
