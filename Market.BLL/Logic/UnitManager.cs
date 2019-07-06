using System.Linq;
using System.Collections.Generic;
using System.Text;
using Market.Abstract;
using Market.Abstract.Logic;
using Market.BLL;
using Market.Entities;
using Market.Models.DTO;

namespace Market.BLL.Logic
{
    public class UnitManager : BaseLogic<UnitDTO, Unit>, IUnitManager
    {
        public UnitManager(IDALContext ctx) : base(ctx)
        {
        }

        /// <summary>
        /// Create initial data for units
        /// </summary>
        public void CreateInitialData()
        {
            if (!db.Units.Any())
            {
                db.Units.AddRange(
                    new Unit
                    {
                        Title = "PC"
                    },
                    new Unit
                    {
                        Title = "KG"
                    },
                    new Unit
                    {
                        Title = "Other"
                    }
                    );
                base.Save();
            }
        }
    }
}
