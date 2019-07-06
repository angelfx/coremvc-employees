using System;
using System.Collections.Generic;
using System.Text;
using Market.Abstract.Service;
using Market.Abstract.Logic;
using Market.Abstract;
using Market.BLL.Logic;

namespace Market.BLL.Service
{
    public class MarketService : IMarketService, IDisposable
    {
        private IDALContext db;
        private bool disposed = false;

        public MarketService(IDALContext ctx)
        {
            db = ctx;
        }

        private IAccountManager _accountManager;

        public IAccountManager AccountManager
        {
            get
            {
                if (_accountManager == null)
                    _accountManager = new AccountManager(db);
                return _accountManager;
            }
        }

        private IUnitManager _unitManager;

        public IUnitManager UnitManager
        {
            get
            {
                if (_unitManager == null)
                    _unitManager = new UnitManager(db);
                return _unitManager;
            }
        }

        private IProductManager _productManager;

        public IProductManager ProductManager
        {
            get
            {
                if (_productManager == null)
                    _productManager = new ProductManager(db);
                return _productManager;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (_accountManager != null)
                    _accountManager.Dispose();
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
