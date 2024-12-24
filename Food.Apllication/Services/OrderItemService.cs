using AutoMapper;
using Food.Apllication.Contracts;
using Food.DTO.Order;
using Food.DTO.Shared;
using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public class OrderItemService : IOrederItemService
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;
        public OrderItemService(IOrderItemRepository _orderItemService,IMapper _mapper)
        {
            orderItemRepository = _orderItemService;
            mapper = _mapper;
        }
        public async Task<ResultView<CreateORUpdateOrderItemDTO>> CreateAsync(CreateORUpdateOrderItemDTO entity)
        {
            ResultView<CreateORUpdateOrderItemDTO> result = new();
            try
            {
                var orderitem = (await orderItemRepository.GetAllAsync()).Any(o => o.OrderId == entity.OrderId && o.ProductId == entity.ProductId);
                if (orderitem)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Already Exists!"
                    };
                    return result;
                }
                var newOne=mapper.Map<OrderItem>(entity);
                await orderItemRepository.CreateAsync(newOne);
                await orderItemRepository.SaveChangesAsync();
                result = new()
                {
                    Entity = entity,
                    ISSuccess = true,
                    Message = "Created Successfully"
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

        public async Task<ResultView<GetAllOrderItemDTO>> DeleteAsync(Guid Id)
        { 

            ResultView<GetAllOrderItemDTO> result = new();
            try
            {
                var oldOne = await orderItemRepository.GetOneAsync(Id);
                if (oldOne == null)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Not Found!"
                    };
                    return result;
                }
                var TempOne = mapper.Map<GetAllOrderItemDTO>(oldOne);
                await orderItemRepository.DeleteAsync(oldOne);
                await orderItemRepository.SaveChangesAsync();
                result = new()
                {
                    Entity = TempOne,
                    ISSuccess = true,
                    Message = "Deleted Susseccfully"
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

        public async Task<List<GetAllOrderItemDTO>> GetAllAsync()
        {
            var data = await orderItemRepository.GetAllAsync();
            var TempData = mapper.Map<List<GetAllOrderItemDTO>>(data);
            return TempData;
        }

        public Task<ResultView<CreateORUpdateOrderItemDTO>> UpdateAsync(CreateORUpdateOrderItemDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
