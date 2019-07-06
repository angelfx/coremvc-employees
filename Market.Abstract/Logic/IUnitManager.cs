using System;
using System.Collections.Generic;
using System.Text;
using Market.Entities;
using Market.Models.DTO;

namespace Market.Abstract.Logic
{
    public interface IUnitManager : IBaseLogic<UnitDTO, Unit>
    {
        void CreateInitialData();
    }
}
