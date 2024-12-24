using Food.DTO.Shared;
using Food.DTO.Shipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public interface IShipmentService
    {
        public Task<ResultView<CreateAndUpdateShipmentDTO>> CreateAsync(CreateAndUpdateShipmentDTO entity);
        public Task<ResultView<CreateAndUpdateShipmentDTO>> UpdateAsync(CreateAndUpdateShipmentDTO entity);
        public Task<EntityPagenated<GetAllShipmentDTO>> GetAllAsync(int PageNumber, int Count);
        public Task<ResultView<GetAllShipmentDTO>> DeleteAsync(Guid Id);
        public Task<ResultView<GetAllShipmentDTO>> GetByIdAsync(Guid Id);
    }
}
