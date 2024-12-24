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
    public class CategoryRepository:GenericRepository<Category,Guid> ,ICategoryRepository
    {
        private readonly FoodContext context;
        public CategoryRepository(FoodContext _context) : base(_context)
        {
            context = _context;
        }
    }
}
