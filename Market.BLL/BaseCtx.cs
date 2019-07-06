using System;
using Market.Abstract;

namespace Market.BLL
{
    /// <summary>
    /// Base class for implement dbcontext
    /// </summary>
    public abstract class BaseCtx : IDisposable
    {
        
        protected IDALContext db;
        protected bool disposed = false;

        public BaseCtx(IDALContext ctx)
        {
            db = ctx;
        }

        public virtual void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                db.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
