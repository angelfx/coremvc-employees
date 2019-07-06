using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Market.MVC.Models;
using Market.Models.DTO;

namespace Market.MVC.Mapper
{
    public class ProductFormProfile : Profile
    {
        public ProductFormProfile()
        {
            CreateMap<ProductFormViewModel, ProductDTO>();
            CreateMap<ProductFormViewModel, UnitDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UnitId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.UnitTitle));
        }

    }
}
