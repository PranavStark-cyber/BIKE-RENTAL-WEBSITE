﻿using BIKERENTALAPI.DTOs.RequestDTO;
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

    }
}