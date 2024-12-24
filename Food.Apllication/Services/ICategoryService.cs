using Food.DTO.Category;
using Food.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public interface ICategoryService
    {
        public Task<ResultView<CreateORupdateCategoryDTO>> CreateAsync(CreateORupdateCategoryDTO entity);
        public Task<ResultView<CreateORupdateCategoryDTO>> UpdateAsync(CreateORupdateCategoryDTO entity);
        public Task<ResultView<GetAllCategoryDTO>> DeleteAsync(Guid Id);
        public Task<GetAllCategoryDTO> GetOneAsync(Guid Id);
        public Task<List<GetAllCategoryDTO>> GetAllCategoryAsync();
        public Task<EntityPagenated<GetAllCategoryDTO>> GetAllAsync(int PageNumber,int Count);
    }
}
