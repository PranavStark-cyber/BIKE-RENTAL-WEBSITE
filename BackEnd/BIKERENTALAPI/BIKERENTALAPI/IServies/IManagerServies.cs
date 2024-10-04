using BIKERENTALAPI.DTOs.RequestDTO;
using BIKERENTALAPI.DTOs.ResponseDTO;

namespace BIKERENTALAPI.IServies
{
    public interface IManagerServies
    {
        Task<ManagerResponseDTO> AddBikeAsync(ManagerRequestDTO bikeRequest);
        Task<ManagerResponseDTO> GetBikeByIdAsync(Guid bikeId);
        Task<List<ManagerResponseDTO>> GetAllBikesAsync();
        Task<bool> DeleteBikeAsync(Guid bikeId);
        Task<ManagerResponseDTO> EditBikeAsync(Guid bikeId, ManagerRequestDTO bikeRequest);
    }
}
