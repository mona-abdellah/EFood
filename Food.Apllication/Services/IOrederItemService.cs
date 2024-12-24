using Food.DTO.Order;
using Food.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public interface IOrederItemService
    {
        public Task<ResultView<CreateORUpdateOrderItemDTO>> CreateAsync(CreateORUpdateOrderItemDTO entity);
        public Task<ResultView<CreateORUpdateOrderItemDTO>> UpdateAsync(CreateORUpdateOrderItemDTO entity);
        public Task<ResultView<GetAllOrderItemDTO>> DeleteAsync(Guid Id);
        public Task<List<GetAllOrderItemDTO>> GetAllAsync();

    }
}
