using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BIKERENTALAPI.Entity;
using BIKERENTALAPI.IRepository;

namespace BIKERENTALAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Customer> GetCustomerByID(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Customers WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if (await reader.ReadAsync())
                {
                    return new Customer
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        Mobilenumber = reader.GetInt32(reader.GetOrdinal("Mobilenumber")),
                        Licence = reader.GetInt32(reader.GetOrdinal("Licence")),
                        Nic = reader.GetInt32(reader.GetOrdinal("Nic")),
                        Password = reader.GetString(reader.GetOrdinal("Password")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
              
                    };
                }
                return null;
            }
        }

        public async Task<Customer> UpdateCustomerByID(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Customers SET FirstName = @FirstName,Mobilenumber=@Mobilenumber,Licence=@Licence,Nic=@Nic,Password=@Password, IsActive = @IsActive WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", customer.Id);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@Mobilenumber", customer.Mobilenumber);
                command.Parameters.AddWithValue("@Licence", customer.Licence);
                command.Parameters.AddWithValue("@Nic", customer.Nic);
                command.Parameters.AddWithValue("@Password", customer.Password);
                command.Parameters.AddWithValue("@IsActive", customer.IsActive);

                await command.ExecuteNonQueryAsync();
                return customer;
            }
        }

        public async Task<Customer> SoftDelete(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Customers SET IsActive = @IsActive WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", customer.Id);
                command.Parameters.AddWithValue("@IsActive", false);

                await command.ExecuteNonQueryAsync();
                customer.IsActive = false;
                return customer;
            }
        }

        public async Task<int> CountActiveCustomers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE IsActive = 1", connection);
                return (int)await command.ExecuteScalarAsync();
            }
        }


        public async Task<Rental> GetRentalByID(Guid rentalId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "SELECT * FROM Rentals WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", rentalId);

                var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if (await reader.ReadAsync())
                {
                    return new Rental
                    {
                        id = reader.GetGuid(reader.GetOrdinal("Id")),
                        CustomerID = reader.GetGuid(reader.GetOrdinal("CustomerID")),
                        MotorbikeID = reader.GetGuid(reader.GetOrdinal("MotorbikeID")),
                        RentalDate = reader.GetDateTime(reader.GetOrdinal("RentalDate")),
                        Returndate = reader.GetDateTime(reader.GetOrdinal("Returndate")),
                        Isoverdue = reader.GetBoolean(reader.GetOrdinal("Isoverdue")),
                        status = reader.GetString(reader.GetOrdinal("status")),
                    };
                }
                return null;
            }
        }

        public async Task<List<Rental>> GetAllRentalsByCustomerID(Guid customerId)
        {
            var rentals = new List<Rental>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "SELECT * FROM Rentals WHERE CustomerID = @CustomerID", connection);
                command.Parameters.AddWithValue("@CustomerID", customerId);

                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    rentals.Add(new Rental
                    {
                        id = reader.GetGuid(reader.GetOrdinal("Id")),
                        CustomerID = reader.GetGuid(reader.GetOrdinal("CustomerID")),
                        MotorbikeID = reader.GetGuid(reader.GetOrdinal("MotorbikeID")),
                        RentalDate = reader.GetDateTime(reader.GetOrdinal("RentalDate")),
                        Returndate = reader.GetDateTime(reader.GetOrdinal("Returndate")),
                        Isoverdue = reader.GetBoolean(reader.GetOrdinal("Isoverdue")),
                        status = reader.GetString(reader.GetOrdinal("status")),

                    });
                }
            }
            return rentals;
        }

        public async Task<List<Customer>> GetAllCustomer()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Customers", connection);

                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    customers.Add(new Customer
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        Mobilenumber = reader.GetInt32(reader.GetOrdinal("Mobilenumber")),
                        Licence = reader.GetInt32(reader.GetOrdinal("Licence")),
                        Nic = reader.GetInt32(reader.GetOrdinal("Nic")),
                        Password = reader.GetString(reader.GetOrdinal("Password")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))

                    });
                }
            }
            return customers;
        }

        public async Task<Customer> Addcustomer(Customer customer)
        {

            if (customer.Id == Guid.Empty)
            {
                customer.Id = Guid.NewGuid();
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Customers (Id, FirstName, Mobilenumber, Licence, Nic, Password, IsActive) " +
                    "VALUES (@Id, @FirstName, @Mobilenumber,@Licence, @Nic, @Password,@IsActive);",
                    connection);


                command.Parameters.AddWithValue("@Id", customer.Id);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@Mobilenumber", customer.Mobilenumber);
                command.Parameters.AddWithValue("@Licence", customer.Licence);
                command.Parameters.AddWithValue("@Nic", customer.Nic);
                command.Parameters.AddWithValue("@Password", customer.Password);
                command.Parameters.AddWithValue("@IsActive", customer.IsActive);

                await command.ExecuteNonQueryAsync();


                return customer;
            }
        }

    }
}
