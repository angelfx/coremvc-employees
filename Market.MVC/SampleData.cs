using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.MVC
{
    public static class SampleData
    {
        public static void Initialize(Market.Abstract.Service.IMarketService marketService)
        {
            marketService.AccountManager.CreateDefaultAdmin();
            marketService.UnitManager.CreateInitialData();
        }
    }
}
