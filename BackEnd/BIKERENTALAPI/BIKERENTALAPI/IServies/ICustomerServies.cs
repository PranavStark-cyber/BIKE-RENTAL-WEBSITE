using BIKERENTALAPI.DTOs.RequestDTO;
using BIKERENTALAPI.DTOs.ResponseDTO;
using BIKERENTALAPI.Entity;

namespace BIKERENTALAPI.IServies
{
    public interface ICustomerServies
    {
        Task<CustomerResponseDTO> GetCustomerByID(Guid id);
        Task<CustomerResponseDTO> UpdateCustomerByID(Guid id, CustomerRequestDTO customerRequestDTO);
        Task<CustomerResponseDTO> SoftDelete(Guid id);
        Task<int> CountActiveCustomers();
        Task<RentalResponseDTO> GetRentalById(Guid id);
        Task<List<RentalResponseDTO>> GetAllRentalsByCustomerId(Guid customerId);
        Task<List<CustomerResponseDTO>> GetAllCustomer();
        Task<CustomerResponseDTO> Addcustomer(CustomerRequestDTO customerRequestDTO);
        Task<RentalResponseDTO> AddRental(RentalRequestDTO rentalRequestDTO);
        Task<RentalResponseDTO> BikeisReturn(Guid id);
        Task<List<RentalResponseDTO>> GetAllRentals();
    }
}
