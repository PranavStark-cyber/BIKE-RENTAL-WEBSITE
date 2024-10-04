using BIKERENTALAPI.DTOs.RequestDTO;
using BIKERENTALAPI.DTOs.ResponseDTO;
using BIKERENTALAPI.Entity;
using BIKERENTALAPI.IRepository;
using BIKERENTALAPI.IServies;
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

    }
}