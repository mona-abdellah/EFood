using AutoMapper;
using Food.Apllication.Contracts;
using Food.DTO.Category;
using Food.DTO.Shared;
using Food.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        
        public CategoryService(ICategoryRepository _categoryRepository,IMapper _mapper,IProductRepository _productRepository)
        {
            categoryRepository = _categoryRepository;
            productRepository = _productRepository;
            mapper = _mapper;
        }
        public async Task<ResultView<CreateORupdateCategoryDTO>> CreateAsync(CreateORupdateCategoryDTO entity)
        {
            ResultView<CreateORupdateCategoryDTO> result = new();
            try
            {
                var cate = (await categoryRepository.GetAllAsync()).Any(c => c.Name == entity.Name);
                if (cate)
                {
                    result = new()
                    {
                        Entity = entity,
                        ISSuccess = false,
                        Message = "This Category already Exist"
                    };
                    return result;
                }
                var uniq = Guid.NewGuid().ToString() + Path.GetExtension(entity.ImageData.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/category", uniq);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await (entity.ImageData).CopyToAsync(stream);
                }
                entity.Image = $"/images/category/{uniq}";
                var NewCat = mapper.Map<Category>(entity);
                await categoryRepository.CreateAsync(NewCat);
                await categoryRepository.SaveChangesAsync();
                result = new()
                {
                    Entity = entity,
                    ISSuccess = true,
                    Message = "Created Successfuly"
                };
                return result;
            }
            catch(Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    ISSuccess = false,
                    Message = "Error " + ex
                };
                return result;
            }
           
        }

        public async Task<ResultView<GetAllCategoryDTO>> DeleteAsync(Guid Id)
        {
            ResultView<GetAllCategoryDTO> result = new();
            try
            {
                var oldOne = (await categoryRepository.GetOneAsync(Id));
                if (oldOne == null)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "There Is No Thing To Delete"
                    };
                    return result;
                }
                else
                {
                    var cate = mapper.Map<GetAllCategoryDTO>(oldOne);
                    var pro = (await productRepository.GetAllAsync()).FirstOrDefault(p => p.CategoryId == Id);
                    if (pro != null)
                    {
                        result = new()
                        {
                            Entity = cate,
                            ISSuccess = false,
                            Message = "Can't Delete It, There is realated Products"
                        };
                        return result;
                    }
                    else
                    {
                        await categoryRepository.DeleteAsync(oldOne);
                        await categoryRepository.SaveChangesAsync();
                        result = new()
                        {
                            Entity =cate,
                            ISSuccess=true,
                            Message="Deleted Successfully"
                        };
                        return result;
                    }
                }
            }
            catch(Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    ISSuccess = false,
                    Message = "Error " + ex
                };
                return result;
            }
            
        }

        public async Task<EntityPagenated<GetAllCategoryDTO>> GetAllAsync(int PageNumber, int Count)
        {
            var allCate=(await  categoryRepository.GetAllAsync()).Skip(Count*(PageNumber-1)).Take(Count).ToList();
            var c = (await categoryRepository.GetAllAsync()).Count();
            var returnedCate = mapper.Map<List<GetAllCategoryDTO>>(allCate);
            EntityPagenated<GetAllCategoryDTO> entityPagenated = new()
            {
                Data=returnedCate,
                Count=c,
                PageSize=Count,
                CurrentPage=PageNumber
            };
            return entityPagenated;          
        }

		public async Task<List<GetAllCategoryDTO>> GetAllCategoryAsync()
		{
            var categories = (await categoryRepository.GetAllAsync())
                .Select(c=> new GetAllCategoryDTO
                { Id=c.Id,
                  Name=c.Name})
                .ToList();
            return categories;
		}

		public async Task<GetAllCategoryDTO> GetOneAsync(Guid Id)
        {
            var category = await categoryRepository.GetOneAsync(Id);
            return mapper.Map<GetAllCategoryDTO>(category);
        }

        public async Task<ResultView<CreateORupdateCategoryDTO>> UpdateAsync(CreateORupdateCategoryDTO entity)
        {
            ResultView<CreateORupdateCategoryDTO> result = new();
            try
            {
                var oldOne = (await categoryRepository.GetAllAsync()).FirstOrDefault(c => c.Id == entity.Id);
                if (oldOne==null)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "There Is No thing to update"
                    };
                    return result;
                }

                if (entity.ImageData != null)
                {
					var uniq = Guid.NewGuid().ToString() + Path.GetExtension(entity.ImageData.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/category", uniq);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await (entity.ImageData).CopyToAsync(stream);
					}
					entity.Image = $"/images/category/{uniq}";
				}
				else
				{ 
                    entity.Image = oldOne.Image;
                }
				mapper.Map(entity,oldOne);
                var updated = await categoryRepository.UpdateAsync(oldOne);
                await categoryRepository.SaveChangesAsync();
                var success=mapper.Map<CreateORupdateCategoryDTO>(updated);
                result = new()
                {
                    Entity = success,
                    ISSuccess = true,
                    Message = "Updated Successfully"
                };
                return result;
            }
            
            catch(Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    ISSuccess = false,
                    Message = "Error " + ex
                };
                return result;
            }
        }
    }
}
