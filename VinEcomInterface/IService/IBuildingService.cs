using VinEcomDomain.Model;

namespace VinEcomInterface.IService
{
    public interface IBuildingService : IBaseService
    {
        Task<Building?> GetBuildingById(int id); 
    }
}
