using System;
using System.Collections.Generic;
using System.Text;
using Market.Abstract;
using Market.Abstract.Logic;
using Market.BLL;
using Market.Entities;
using Market.Models.DTO;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Market.BLL.Logic
{
    public class ProductManager : BaseLogic<ProductDTO, Product>, IProductManager
    {
        public ProductManager(IDALContext ctx) : base(ctx)
        {

        }

        /// <summary>
        /// Overrida GetAll method
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ProductDTO> GetAll()
        {
            var items = db.Products.Include(x => x.Unit).ToList();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<Market.BLL.Mapper.ProductProfile>());
            var mapper = config.CreateMapper();
            var model = mapper.Map<List<ProductDTO>>(items);

            return model;
        }

        public TableProductDTO GetPaged(int page, int pageSize)
        {
            var items = db.Products.Include(x => x.Unit).Skip((page - 1) * pageSize).Take(pageSize + 1).ToList();
            TableProductDTO model = new TableProductDTO();
            model.NextPage = items.Count() > pageSize;

            var config = new MapperConfiguration(cfg => cfg.AddProfile<Market.BLL.Mapper.ProductProfile>());
            var mapper = config.CreateMapper();
            model.Products = mapper.Map<List<ProductDTO>>(items.Take(pageSize));

            return model;
        }

        public override ProductDTO Get(int id)
        {
            var item = db.Products.Include(x => x.Unit).FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<Market.BLL.Mapper.ProductProfile>());
                var mapper = config.CreateMapper();
                var model = mapper.Map<ProductDTO>(item);

                return model;
            }
            return null;
        }

        public override void Update(ProductDTO item)
        {
            if (item != null)
            {
                if (item.Id <= 0)
                    return;
                var config = new MapperConfiguration(cfg => cfg.AddProfile<Market.BLL.Mapper.ProductProfile>());
                var mapper = config.CreateMapper();
                var model = mapper.Map<Product>(item);

                var unit = db.Units.Find(item.Unit.Id);
                model.Unit = unit;

                db.Products.Update(model);

                base.Save();
            }
        }

        public override void Create(ProductDTO item)
        {
            if (item != null)
            {
                if (db.Products.Any(x => x.Id == item.Id))
                    return;
                var config = new MapperConfiguration(cfg => cfg.AddProfile<Market.BLL.Mapper.ProductProfile>());
                var mapper = config.CreateMapper();
                var model = mapper.Map<Product>(item);

                db.Products.Add(model);

                base.Save();
            }
        }
    }
}
