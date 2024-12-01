using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Contracts
{
    public interface IPaymentRepository:IGenericRepository<Payment,Guid>
    {
    }
}
