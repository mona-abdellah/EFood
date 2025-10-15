using AutoMapper;
using Food.Apllication.Contracts;
using Food.DTO.Order;
using Food.DTO.Shared;
using Food.DTO.Shipment;
using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepositotory orderRepository;
        private readonly IMapper mapper;
        public OrderService(IOrderRepositotory _orderRepository,IMapper _mapper)
        {
            orderRepository = _orderRepository;
            mapper = _mapper;
        }
        public async Task<ResultView<CreateORUpdateOrderDTO>> CreateAsync(CreateORUpdateOrderDTO entity)
        {
            ResultView<CreateORUpdateOrderDTO> result = new();
            try
            {
                var order = (await orderRepository.GetAllAsync()).Any(o => o.Id == entity.Id && o.OrderDate == entity.OrderDate);
                if (order)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Already Exists"
                    };
                    return result;
                }
                else
                {
                    var newOne = mapper.Map<Order>(entity);
                    await orderRepository.CreateAsync(newOne);
                    await orderRepository.SaveChangesAsync();
                    result = new()
                    {
                        Entity = entity,
                        ISSuccess = true,
                        Message = "Created Successfully"
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

        public async Task<ResultView<GetAllOrderDTO>> DeleteAsync(Guid Id)
        {
            ResultView<GetAllOrderDTO> result = new();
            try
            {
                var oldOne = await orderRepository.GetOneAsync(Id);
                if (oldOne == null)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Not Found"
                    };
                    return result;
                }
                var returned = mapper.Map<GetAllOrderDTO>(oldOne);
                await orderRepository.DeleteAsync(oldOne);
                await orderRepository.SaveChangesAsync();
                result = new()
                {
                    Entity = returned,
                    ISSuccess = true,
                    Message = "Deleted Successfully"
                };
                return result;
            }
            catch (Exception ex)
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

        public async Task<List<GetAllOrderDTO>> GetAll()
        {
            var data = (await orderRepository.GetAllAsync()).ToList();
            var tempData = mapper.Map <List<GetAllOrderDTO>> (data);
            return tempData;
        }

        public async Task<EntityPagenated<GetAllOrderDTO>> GetAllAsync(int PageNumber, int Count)
        {
            var data = (await orderRepository.GetAllAsync()).OrderBy(o=>o.Create).Select(o=>new GetAllOrderDTO
            {
                Id=o.Id,
                OrderDate=o.OrderDate,
                TotalPrice=o.TotalPrice,
                Status=o.Status,
                CustomerEmail=o.Customer.Email,
                CustomerName=o.Customer.FName,
                customerId=o.customerId,
                PaymentId=o.PaymentId,
                shipmentId=o.shipmentId
            }).Skip(Count * (PageNumber - 1)).Take(Count).ToList();
            var c = (await orderRepository.GetAllAsync()).Count();
            EntityPagenated<GetAllOrderDTO> entityPagenated = new()
            {
                Data = data,
                Count = c,
                CurrentPage = PageNumber,
                PageSize = Count
            };
            return entityPagenated;
            
        }

        public async Task<ResultView<GetAllOrderDTO>> GetOneAsync(Guid Id)
        {
           
            ResultView<GetAllOrderDTO> result = new();
            try
            {
                var order = await orderRepository.GetOneAsync(Id);
                if (order == null)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Not Found"
                    };
                    return result;
                }
                else
                {
                    var ReturnOrder = mapper.Map<GetAllOrderDTO>(order);
                    result = new()
                    {
                        Entity = ReturnOrder,
                        ISSuccess = true,
                        Message = "Returned Successfullly"
                    };
                    return result;
                }
            }
            catch (Exception ex)
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

        public async Task<ResultView<CreateORUpdateOrderDTO>> UpdateAsync(CreateORUpdateOrderDTO entity)
        {
            ResultView<CreateORUpdateOrderDTO> result = new();
            try
            {
                var oldOne = (await orderRepository.GetAllAsync()).Any(s => s.Id == entity.Id);
                if (oldOne == null)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Not Found"
                    };
                    return result;
                }
                var order = mapper.Map<Order>(entity);
                await orderRepository.UpdateAsync(order);
                await orderRepository.SaveChangesAsync();
                result = new()
                {
                    Entity = entity,
                    ISSuccess = true,
                    Message = "Updated Successfully"
                };
                return result;
            }
            catch (Exception ex)
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
