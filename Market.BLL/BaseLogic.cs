using System;
using System.Collections.Generic;
using System.Text;
using Market.Abstract;
using AutoMapper;
using System.Linq;

namespace Market.BLL
{
    /// <summary>
    /// Base class for implement CRUD-logic
    /// </summary>
    /// <typeparam name="TEntity">Entity of dbContext</typeparam>
    /// <typeparam name="T">DTO model</typeparam>
    public abstract class BaseLogic<T, TEntity> : BaseCtx, IBaseLogic<T, TEntity> where TEntity : class where T : class
    {
        public BaseLogic(IDALContext ctx) : base(ctx)
        {
        }

        public virtual IEnumerable<T> GetAll()
        {
            var items = db.Set<TEntity>().ToList();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, T>());
            var mapper = config.CreateMapper();
            var model = mapper.Map<List<T>>(items);

            return model;
        }

        public virtual T Get(int id)
        {
            var itemEntity = db.Set<TEntity>().Find(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, T>());
            var mapper = config.CreateMapper();

            var item = mapper.Map<T>(itemEntity);

            return item;
        }

        public virtual void Create(T item)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, TEntity>());
            var mapper = config.CreateMapper();

            var itemEntity = mapper.Map<TEntity>(item);

            db.Set<TEntity>().Add(itemEntity);
            base.Save();
        }

        public virtual void Update(T item)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, TEntity>());
            var mapper = config.CreateMapper();

            var itemEntity = mapper.Map<TEntity>(item);

            db.Set<TEntity>().Update(itemEntity);
            base.Save();
        }

        public virtual bool Delete(int id)
        {
            var item = db.Set<TEntity>().Find(id);
            if (item != null)
            {
                db.Set<TEntity>().Remove(item);
                base.Save();
                return true;
            }
            return false;

        }
    }
}
