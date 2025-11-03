using Food.Apllication.Contracts;
using Food.DTO.Product;
using Food.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Food.Models;
using Food.Apllication.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
namespace Food.Apllication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IOrderItemRepository orderItemRepository;
        public ProductService(IProductRepository _productRepository,IMapper _mapper,IOrderItemRepository _orderItemRepository)
        {
            productRepository = _productRepository;
            mapper = _mapper;
            orderItemRepository = _orderItemRepository;
        }
        public async Task<ResultView<CreateORUpdateProductDTO>> CreateAsync(CreateORUpdateProductDTO entity)
        {
            ResultView<CreateORUpdateProductDTO> result = new ResultView<CreateORUpdateProductDTO>();
            try
            {
               var existing = (await productRepository.GetAllAsync(p => p.name == entity.name))
                                .FirstOrDefault();

                if (existing != null)
                {
                    if (existing.IsDeleted == true)
                    {
                        existing.IsDeleted = false;
                        existing.Updated = DateTime.Now;

                        await productRepository.UpdateAsync(existing); 
                        await productRepository.SaveChangesAsync();

                        var returnproduct = mapper.Map<CreateORUpdateProductDTO>(existing);
                        result = new()
                        {
                            Entity = returnproduct,
                            ISSuccess = true,
                            Message = "Product Restored Successfully"
                        };
                        return result;
                    }
                    else
                    {
                        result = new()
                        {
                            Entity = null,
                            ISSuccess = false,
                            Message = "This Product Exists"
                        };
                        return result;
                    }
                }
                var uniq = Guid.NewGuid().ToString() + Path.GetExtension(entity.ImageData.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", uniq);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await (entity.ImageData).CopyToAsync(stream);
                }
                entity.Image = $"/images/products/{uniq}";

                var pro = mapper.Map<Product>(entity);
                pro.Create = DateTime.Now;
                pro.IsDeleted = false;

                var success = await productRepository.CreateAsync(pro);
                await productRepository.SaveChangesAsync();

                var returnproductNew = mapper.Map<CreateORUpdateProductDTO>(success);
                result = new()
                {
                    Entity = returnproductNew,
                    ISSuccess = true,
                    Message = "Created Successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    ISSuccess = false,
                    Message = "Error " + ex.Message
                };
                return result;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {   
            var entity=await productRepository.GetOneAsync(Id);
            if (entity == null)
                return false;
            entity.IsDeleted = true;
            await productRepository.UpdateAsync(entity);
            return await productRepository.SaveChangesAsync()>0;         
        }

        public async Task<EntityPagenated<GetAllProductDTO>> GetAllAsync(int PageNumber, int Count)
        {
            var c = (await productRepository.GetAllAsync()).Count();
            var data =(await productRepository.GetAllAsync(p=> p.IsDeleted==false)).OrderBy(p=>p.Create).Select(p=>new GetAllProductDTO
            {
                Id=p.Id,
                name=p.name,
                stock=p.stock,
                price=p.price,
                categoryName=p.Category.Name,
                Image=p.Image
            }).Skip(Count * (PageNumber - 1)).Take(Count).ToList();
            EntityPagenated<GetAllProductDTO> entityPagenated = new()
            {
                Data = data,
                Count = c,
                PageSize = Count,
                CurrentPage = PageNumber
            };
            return entityPagenated;
                
        }

        public async Task<EntityPagenated<GetAllProductDTO>> GetAllByCatId(Guid CatId, int PageNumber, int Count)
        {
            var c = (await productRepository.GetAllAsync(p=>p.CategoryId==CatId)).Count();
            var data = (await productRepository.GetAllAsync(p => p.IsDeleted == false)).Where(p=>p.CategoryId==CatId).Select(p => new GetAllProductDTO
            {
                Id = p.Id,
                name = p.name,
                stock = p.stock,
                price = p.price,
                categoryName = p.Category.Name,
                Image = p.Image
            }).Skip(Count * (PageNumber - 1)).Take(Count).ToList();
            EntityPagenated<GetAllProductDTO> entityPagenated = new()
            {
                Data = data,
                Count = c,
                PageSize = Count,
                CurrentPage = PageNumber
            };
            return entityPagenated;
        }

        public async Task<CreateORUpdateProductDTO> GetOneAsync(Guid Id)
		{
            var product = await productRepository.GetOneAsync(Id);
            return mapper.Map<CreateORUpdateProductDTO>(product);
		}

		public async Task<ResultView<CreateORUpdateProductDTO>> UpdateAsync(CreateORUpdateProductDTO entity)
        {
            ResultView<CreateORUpdateProductDTO> result = new ResultView<CreateORUpdateProductDTO>();
            try
            {
                var existing = await productRepository.GetOneAsync(entity.Id);

                if (existing == null || existing.IsDeleted == true)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Product Not Found or Deleted"
                    };
                    return result;
                }
                if (entity.ImageData != null)
				{
					var uniq = Guid.NewGuid().ToString() + Path.GetExtension(entity.ImageData.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", uniq);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await (entity.ImageData).CopyToAsync(stream);
					}
					entity.Image = $"/images/products/{uniq}";
				}
				else
				{
					entity.Image = existing.Image;
				}
				mapper.Map(entity, existing);
				var updated=await productRepository.UpdateAsync(existing);
                await productRepository.SaveChangesAsync();
                var pro = mapper.Map<CreateORUpdateProductDTO>(updated);
                result = new()
                {
                    Entity = pro,
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
