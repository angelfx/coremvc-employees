using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Market.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Market.Abstract.Service;
using Market.Models.DTO;
using AutoMapper;

namespace Market.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMarketService ctx) : base(ctx) { }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            var productsDTO = service.ProductManager.GetPaged(page, pageSize);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTO, ProductViewModel>()
                    .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit.Id))
                    .ForMember(dest => dest.UnitTitle, opt => opt.MapFrom(src => src.Unit.Title));
                cfg.CreateMap<TableProductDTO, TableProductViewModel>();
            });
            var mapper = config.CreateMapper();
            var model = mapper.Map<TableProductViewModel>(productsDTO);
            model.Page = page;
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var productDTO = service.ProductManager.Get(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()
               .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit.Id))
               .ForMember(dest => dest.UnitTitle, opt => opt.MapFrom(src => src.Unit.Title)));
            var mapper = config.CreateMapper();

            return View(mapper.Map<ProductViewModel>(productDTO));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
