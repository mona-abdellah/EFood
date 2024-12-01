using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Contracts
{
    public interface IFavoriteRepository:IGenericRepository<Favorite,Guid>
    {
    }
}
