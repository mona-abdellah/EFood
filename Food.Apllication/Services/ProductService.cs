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
                bool product = (await productRepository.GetAllAsync()).Any(p=>p.name==entity.name);
                if (product)
                {
                    result = new()
                    {
                        Entity =null,
                        ISSuccess=false,
                        Message="This Product Exists"
                    };
                    return result;
                }
                else
                {
					var uniq = Guid.NewGuid().ToString() + Path.GetExtension(entity.ImageData.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", uniq);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await (entity.ImageData).CopyToAsync(stream);
					}
					entity.Image = $"/images/products/{uniq}";

					var pro = mapper.Map<Product>(entity);
                    var success = await productRepository.CreateAsync(pro);
                    await productRepository.SaveChangesAsync();
                    var returnproduct = mapper.Map<CreateORUpdateProductDTO>(success);
                    result = new()
                    {
                        Entity=returnproduct,
                        ISSuccess=true,
                        Message="Created Successfully"
                    };
                    return result;
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

        public async Task<ResultView<GetAllProductDTO>> DeleteAsync(Guid Id)
        {
            ResultView<GetAllProductDTO> result = new ResultView<GetAllProductDTO>();
            try
            {
                var pro = (await productRepository.GetOneAsync(Id));
                var allorder = (await orderItemRepository.GetAllAsync());
                var order=await allorder.AnyAsync(o => o.ProductId == Id);
                if (order)
                {
                    result = new()
                    {
                        Entity=null,
                        ISSuccess=false,
                        Message="Can't Delete This Product Beacause There is related Order"
                    };
                    return result;
                }
                else
                {
                    var product = mapper.Map<GetAllProductDTO>(pro);
                    await productRepository.DeleteAsync(pro);
                    await productRepository.SaveChangesAsync();
                    
                    result = new()
                    {
                        Entity = product,
                        ISSuccess = true,
                        Message = "Deleted Successfully"
                    };
                    return result;
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

        public async Task<EntityPagenated<GetAllProductDTO>> GetAllAsync(int PageNumber, int Count)
        {
            var data = (await productRepository.GetAllAsync()).Skip(Count * (PageNumber - 1)).Take(Count).ToList();
            var c = (await productRepository.GetAllAsync()).Count();
            var returnData = mapper.Map<List<GetAllProductDTO>>(data);
            EntityPagenated<GetAllProductDTO> entityPagenated = new()
            {
                Data = returnData,
                Count = c,
                PageSize = Count,
                CurrentPage = PageNumber
            };
            return entityPagenated;
                
        }

		public async Task<GetAllProductDTO> GetOneAsync(Guid Id)
		{
            var product = await productRepository.GetOneAsync(Id);
            return mapper.Map<GetAllProductDTO>(product);
		}

		public async Task<ResultView<CreateORUpdateProductDTO>> UpdateAsync(CreateORUpdateProductDTO entity)
        {
            ResultView<CreateORUpdateProductDTO> result = new();
            try
            {
                var oldOne = (await productRepository.GetAllAsync()).FirstOrDefault(p => p.Id == entity.Id);
                if (oldOne == null)
                {
                    result = new()
                    {
                        Entity=null,
                        ISSuccess=false,
                        Message="There Is No Products"
                    };
                    return result;
                }
                else
                {
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
						entity.Image = oldOne.Image;
					}
					mapper.Map(entity, oldOne);
					var updated=await productRepository.UpdateAsync(oldOne);
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
