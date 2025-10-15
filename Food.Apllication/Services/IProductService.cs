using Food.Apllication.Contracts;
using Food.DTO.Customer;
using Food.DTO.Product;
using Food.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public interface IProductService
    {
        public Task<ResultView<CreateORUpdateProductDTO>> CreateAsync(CreateORUpdateProductDTO productDTO);

        public Task<ResultView<CreateORUpdateProductDTO>> UpdateAsync(CreateORUpdateProductDTO productDTO);
        public Task<EntityPagenated<GetAllProductDTO>> GetAllAsync(int PageNumber, int Count);
        public Task<CreateORUpdateProductDTO> GetOneAsync(Guid Id);
        public Task<bool> DeleteAsync(Guid Id);
        public Task<EntityPagenated<GetAllProductDTO>> GetAllByCatId(Guid CatId, int PageNumber, int Count);
    }
}
