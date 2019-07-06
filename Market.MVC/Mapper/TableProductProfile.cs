using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Market.MVC.Models;
using Market.Models.DTO;

namespace Market.MVC.Mapper
{
    public class TableProductProfile : Profile
    {
        public TableProductProfile()
        {
            CreateMap<ProductDTO, ProductViewModel>()
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit.Id))
                .ForMember(dest => dest.UnitTitle, opt => opt.MapFrom(src => src.Unit.Title));
            CreateMap<TableProductDTO, TableProductViewModel>();
            CreateMap<ProductDTO, ProductFormViewModel>()
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit.Id))
                .ForMember(dest => dest.UnitTitle, opt => opt.MapFrom(src => src.Unit.Title));
        }
    }
}
