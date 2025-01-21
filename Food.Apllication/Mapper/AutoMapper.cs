using AutoMapper;
using Food.DTO.Category;
using Food.DTO.Customer;
using Food.DTO.Order;
using Food.DTO.Product;
using Food.DTO.Shipment;
using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateORupdateCategoryDTO, Category>().ReverseMap();
            CreateMap<CreateORUpdateCustomerDTO,Customer>().ReverseMap();
            CreateMap<CreateORUpdateProductDTO, Product>().ReverseMap();
            CreateMap<GetAllProductDTO, Product>().ReverseMap();
            CreateMap<CreateORupdateCategoryDTO, Category>().ReverseMap();
            CreateMap<GetAllCategoryDTO, Category>().ReverseMap();
            CreateMap<CreateAndUpdateShipmentDTO, Shipment>().ReverseMap();
            CreateMap<GetAllShipmentDTO, Shipment>().ReverseMap();
            CreateMap<CreateORUpdateOrderDTO, Order>().ReverseMap();
            CreateMap<GetAllOrderDTO, Order>().ReverseMap();
            CreateMap<CreateORUpdateOrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<GetAllOrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<AdminDTO, Customer>().ReverseMap();
            CreateMap<AdminLoginDTO, Customer>().ReverseMap();
        }
    }
}
