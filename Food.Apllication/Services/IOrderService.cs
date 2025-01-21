using Food.DTO.Order;
using Food.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public interface IOrderService
    {
        public Task<ResultView<CreateORUpdateOrderDTO>> CreateAsync(CreateORUpdateOrderDTO entity);
        public Task<ResultView<CreateORUpdateOrderDTO>> UpdateAsync(CreateORUpdateOrderDTO entity);
        public Task<EntityPagenated<GetAllOrderDTO>> GetAllAsync(int PageNumber, int Count);
        public Task<ResultView<GetAllOrderDTO>> GetOneAsync(Guid Id);
        public Task<ResultView<GetAllOrderDTO>> DeleteAsync(Guid Id);
        public Task<List<GetAllOrderDTO>> GetAll();
    }
}
