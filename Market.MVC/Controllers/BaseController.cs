using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Market.Abstract.Service;

namespace Market.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IMarketService service;

        public BaseController(IMarketService ctx)
        {
            service = ctx;
        }
    }
}