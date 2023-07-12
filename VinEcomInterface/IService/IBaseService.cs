using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomViewModel.Base;

namespace VinEcomInterface.IService
{
    public interface IBaseService
    {
        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<Building?> GetBuildingById(int id);
    }
}
