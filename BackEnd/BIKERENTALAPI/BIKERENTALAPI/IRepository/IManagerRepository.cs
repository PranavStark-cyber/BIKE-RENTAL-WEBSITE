using BIKERENTALAPI.Entity;

namespace BIKERENTALAPI.IRepository
{
    public interface IManagerRepository
    {
        Task<Bikes> AddBike(Bikes bike);
        Task<Bikes> GetBikeById(Guid id);
        Task<List<Bikes>> GetAllBikes();
        Task<Bikes> DeleteBike(Guid id);
        Task<Bikes> EditBike(Bikes bike);
    }
}
