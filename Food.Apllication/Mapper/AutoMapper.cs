using AutoMapper;
using Food.DTO.Category;
using Food.DTO.Customer;
using Food.DTO.Product;
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

        }
    }
}
