using BIKERENTALAPI.DTOs.RequestDTO;
using BIKERENTALAPI.DTOs.ResponseDTO;
using BIKERENTALAPI.Entity;
using BIKERENTALAPI.IRepository;
using BIKERENTALAPI.IServies;
using BIKERENTALAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIKERENTALAPI.Servies
{
    public class CustomerServies : ICustomerServies
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServies(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponseDTO> GetCustomerByID(Guid id)
        {
            var data = await _customerRepository.GetCustomerByID(id);

            var customerObj = new CustomerResponseDTO
            {
                Id = id,
                FirstName = data.FirstName,
                Mobilenumber = data.Mobilenumber,
                Licence = data.Licence,
                Nic = data.Nic,
                Password = data.Password,
                Rental_history = data.Rental_history
            };

            return customerObj;
        }

        public async Task<CustomerResponseDTO> UpdateCustomerByID(Guid id, CustomerRequestDTO customerRequestDTO)
        {
            var customerreque = new Customer
            {
                Id = id,
                FirstName = customerRequestDTO.FirstName,
                Mobilenumber = customerRequestDTO.Mobilenumber,
                Licence = customerRequestDTO.Licence,
                Password = customerRequestDTO.Password,
                Nic = customerRequestDTO.Nic,
            };

            var data = await _customerRepository.UpdateCustomerByID(customerreque);

            var customerObj = new CustomerResponseDTO
            {
                Id = id,
                FirstName = data.FirstName,
                Mobilenumber = data.Mobilenumber,
                Licence = data.Licence,
                Nic = data.Nic,
                Password = data.Password,
                Rental_history = data.Rental_history
            };

            return customerObj;
        }

        public async Task<CustomerResponseDTO> SoftDelete(Guid id)
        {
            var customerdata = await _customerRepository.GetCustomerByID(id);
            var data = await _customerRepository.SoftDelete(customerdata);

            var customerObj = new CustomerResponseDTO
            {
                Id = id,
                FirstName = data.FirstName,
                Mobilenumber = data.Mobilenumber,
                Licence = data.Licence,
                Nic = data.Nic,
                Password = data.Password,
                Rental_history = data.Rental_history
            };

            return customerObj;
        }

        public async Task<int> CountActiveCustomers()
        {
            var data = await _customerRepository.CountActiveCustomers();
            return data;
        }

        public async Task<RentalResponseDTO> GetRentalById(Guid id)
        {
            var data = await _customerRepository.GetRentalByID(id);
            var rentalresp = new RentalResponseDTO
            {
                id = data.id,
                CustomerID = data.CustomerID,
                MotorbikeID = data.MotorbikeID,
                Returndate = data.Returndate,
                status = data.status,
                Isoverdue = data.Isoverdue,
                RentalDate = data.RentalDate,
            };

            return rentalresp;
        }

        public async Task<List<RentalResponseDTO>> GetAllRentalsByCustomerId(Guid customerId)
        {
            var rentals = await _customerRepository.GetAllRentalsByCustomerID(customerId);

            var rentaldata = new List<RentalResponseDTO>();

            foreach (var data in rentals)
            {
                var rentalresp = new RentalResponseDTO
                {
                    id = data.id,
                    CustomerID = data.CustomerID,
                    MotorbikeID = data.MotorbikeID,
                    Returndate = data.Returndate,
                    status = data.status,
                    Isoverdue = data.Isoverdue,
                    RentalDate = data.RentalDate,
                };

                rentaldata.Add(rentalresp);
            }
            return rentaldata;
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomer()
        {
            var customer = await _customerRepository.GetAllCustomer();

            var data = new List<CustomerResponseDTO>();
            foreach (var item in customer)
            {
                var customerrespo = new CustomerResponseDTO
                {
                    FirstName = item.FirstName,
                    Nic = item.Nic,
                    Id = item.Id,
                    Mobilenumber = item.Mobilenumber,
                    Licence = item.Licence,
                    Rental_history = item.Rental_history,
                    Password = item.Password,
                };

                data.Add(customerrespo);
            }

            return data;
        }

        public async Task<CustomerResponseDTO> Addcustomer(CustomerRequestDTO customerRequestDTO)
        {
            var customerreques = new Customer
            {
                FirstName = customerRequestDTO.FirstName,
                Licence = customerRequestDTO.Licence,
                Nic = customerRequestDTO.Nic,
                Mobilenumber = customerRequestDTO.Mobilenumber,
                Password = customerRequestDTO.Password
            };

            var data = await _customerRepository.Addcustomer(customerreques);

            var customerrespo = new CustomerResponseDTO
            {
                FirstName = data.FirstName,
                Licence = data.Licence,
                Nic = data.Nic,
                Id = data.Id,
                Mobilenumber = data.Mobilenumber,
                Password = data.Password,
            };

            return customerrespo;
        }

        public async Task<RentalResponseDTO> AddRental(RentalRequestDTO rentalRequestDTO)
        {
            var rental = new Rental
            {
                CustomerID = rentalRequestDTO.CustomerID,
                MotorbikeID = rentalRequestDTO.MotorbikeID,
                Returndate = rentalRequestDTO.Returndate,
                RentalDate = rentalRequestDTO.RentalDate,
            };

            var data = await _customerRepository.AddRental(rental);

            var rentalresp = new RentalResponseDTO
            {
                id = data.id,
                CustomerID = data.CustomerID,
                MotorbikeID = data.MotorbikeID,
                Returndate = data.Returndate,
                status = data.status,
                Isoverdue = data.Isoverdue,
                RentalDate = data.RentalDate,
            };

            return rentalresp;
        }

        public async Task<RentalResponseDTO> RentalAccept(Guid id)
        {
            var Rentdata = await _customerRepository.GetRentalByID(id);
            if (Rentdata.status == "Pending")
            {
                var data = await _customerRepository.RentalAccept(Rentdata);

                var RentalRespon = new RentalResponseDTO
                {
                    id = id,
                    RentalDate = data.RentalDate,
                    Returndate = data.Returndate,
                    MotorbikeID = data.MotorbikeID,
                    CustomerID = data.CustomerID,
                    Isoverdue = data.Isoverdue,
                    status = data.status
                };

                return RentalRespon;
            }
            else
            {
                return null;
            }
        }

        public async Task<RentalResponseDTO> BikeisReturn(Guid id)
        {
            var Rentdata = await _customerRepository.GetRentalByID(id);
            if (Rentdata.status == "Rent")
            {
                var data = await _customerRepository.UpdaterenttoReturn(Rentdata);

                var RentalRespon = new RentalResponseDTO
                {
                    id = id,
                    RentalDate = data.RentalDate,
                    Returndate = data.Returndate,
                    MotorbikeID = data.MotorbikeID,
                    CustomerID = data.CustomerID,
                    Isoverdue = data.Isoverdue,
                    status = data.status
                };

                return RentalRespon;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<RentalResponseDTO>> GetAllRentals()
        {
            var customer = await _customerRepository.GetAllRentals();

            var data = new List<RentalResponseDTO>();
            foreach (var item in customer)
            {
                var rentalrespo = new RentalResponseDTO
                {
                    id = item.id,
                    MotorbikeID = item.MotorbikeID,
                    CustomerID = item.CustomerID,
                    RentalDate = item.RentalDate,
                    Returndate = item.Returndate,
                    Isoverdue = item.Isoverdue,
                    status = item.status,
                };

                data.Add(rentalrespo);
            }

            return data;
        }


        public async Task<bool> RejectRenatal(Guid rentalid)
        {
            var rental = await _customerRepository.GetRentalByID(rentalid);
            if (rental == null) return false;

            await _customerRepository.RejectRenatal(rentalid);
            return true;
        }

        public async Task<List<Guid>> CheckAndUpdateOverdueRentals()
        {
            var overdue = await _customerRepository.CheckAndUpdateOverdueRentals();
            if (overdue == null) return null;
           return overdue;           
        }
    }
}