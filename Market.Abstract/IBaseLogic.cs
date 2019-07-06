using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Abstract
{
    /// <summary>
    /// Interface for base CRUD-logic
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseLogic<T, TEntity> : IDisposable where TEntity : class where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        bool Delete(int id);
        void Save();
    }
}
