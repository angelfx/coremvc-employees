using System;
using System.Collections.Generic;
using System.Text;
using Market.Abstract.Logic;

namespace Market.Abstract.Service
{
    public interface IMarketService
    {
        IAccountManager AccountManager { get; }

        IUnitManager UnitManager { get; }

        IProductManager ProductManager { get; }
    }
}
