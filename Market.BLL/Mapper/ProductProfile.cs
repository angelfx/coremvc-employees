using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Market.Models.DTO;
using Market.Entities;

namespace Market.BLL.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Unit, UnitDTO>();
            CreateMap<UnitDTO, Unit>();
        }
    }
}
