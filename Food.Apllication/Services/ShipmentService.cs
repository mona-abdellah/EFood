using AutoMapper;
using Food.Apllication.Contracts;
using Food.DTO.Shared;
using Food.DTO.Shipment;
using Food.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository shipmentRepository;
        private readonly IOrderRepositotory orderRepository;
        private readonly IMapper mapper;
        public ShipmentService(IShipmentRepository _shipmentRepository,IMapper _mapper,IOrderRepositotory _orderRepository)
        {
            shipmentRepository = _shipmentRepository;
            orderRepository = _orderRepository;
            mapper = _mapper;
        }
        public async Task<ResultView<CreateAndUpdateShipmentDTO>> CreateAsync(CreateAndUpdateShipmentDTO entity)
        {
            ResultView<CreateAndUpdateShipmentDTO> result = new();
            try
            {
                var ship = mapper.Map<Shipment>(entity);
                await shipmentRepository.CreateAsync(ship);
                await shipmentRepository.SaveChangesAsync();
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

        public async Task<ResultView<GetAllShipmentDTO>> DeleteAsync(Guid Id)
        {
            ResultView<GetAllShipmentDTO> result = new();
            try
            {
                var ship = (await shipmentRepository.GetOneAsync(Id));
                var orders = (await orderRepository.GetAllAsync());
                var order = await orders.AnyAsync(s => s.Id == Id);
                if (!order)
                {
                    result = new()
                    {
                        Entity = null,
                        ISSuccess = false,
                        Message = "Can't Delete"
                    };
                    return result;
                }
                var returnedship = mapper.Map<GetAllShipmentDTO>(ship);
                await shipmentRepository.DeleteAsync(ship);
                await shipmentRepository.SaveChangesAsync();
                result = new()
                {
                    Entity = returnedship,
                    ISSuccess = true,
                    Message="Deleted Successfuly"
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

        public Task<EntityPagenated<GetAllShipmentDTO>> GetAllAsync(int PageNumber, int Count)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultView<GetAllShipmentDTO>> GetByIdAsync(Guid Id)
        {
            ResultView<GetAllShipmentDTO> result =new();
            try
            {
                var ship = await shipmentRepository.GetOneAsync(Id);
                if (ship == null)
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
                    var ReturnShip = mapper.Map<GetAllShipmentDTO>(ship);
                    result = new()
                    {
                        Entity = ReturnShip,
                        ISSuccess = true,
                        Message = "Returned Successfullly"
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

        public async Task<ResultView<CreateAndUpdateShipmentDTO>> UpdateAsync(CreateAndUpdateShipmentDTO entity)
        {
            ResultView<CreateAndUpdateShipmentDTO> result = new();
            try
            {
                var oldOne = (await shipmentRepository.GetAllAsync()).Any(s => s.Id == entity.Id);
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
                var ship = mapper.Map<Shipment>(entity);
                await shipmentRepository.UpdateAsync(ship);
                await shipmentRepository.SaveChangesAsync();
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
