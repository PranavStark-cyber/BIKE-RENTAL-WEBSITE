using BIKERENTALAPI.Entity;

namespace BIKERENTALAPI.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByID(Guid id);
        Task<Customer> UpdateCustomerByID(Customer customer);
        Task<Customer> SoftDelete(Customer customer);
        Task<int> CountActiveCustomers();
        Task<Rental> GetRentalByID(Guid rentalId);
        Task<List<Rental>> GetAllRentalsByCustomerID(Guid customerId);
        Task<List<Customer>> GetAllCustomer();
        Task<Customer> Addcustomer(Customer customer);
        Task<Rental> AddRental(Rental rentals);
        Task<Rental> UpdaterenttoReturn(Rental rental);
        Task<List<Rental>> GetAllRentals();


    }
}
