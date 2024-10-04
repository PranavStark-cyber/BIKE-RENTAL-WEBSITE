using BIKERENTALAPI.DTOs.RequestDTO;
using BIKERENTALAPI.DTOs.ResponseDTO;
using BIKERENTALAPI.IServies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIKERENTALAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServies _customerServies;

        public CustomerController(ICustomerServies customerServies)
        {
            _customerServies = customerServies;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByID(Guid id)
        {
            try
            {
                var result = await _customerServies.GetCustomerByID(id);
                if (result == null)
                    return NotFound("Customer not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerByID(Guid id, CustomerRequestDTO customerRequestDTO)
        {
            try
            {
                var result = await _customerServies.UpdateCustomerByID(id, customerRequestDTO);
                if (result == null)
                    return NotFound("Customer not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            try
            {
                var result = await _customerServies.SoftDelete(id);
                if (result == null)
                    return NotFound("Customer not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("active-customers-count")]
        public async Task<IActionResult> CountActiveCustomers()
        {
            try
            {
                var count = await _customerServies.CountActiveCustomers();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("rental/{id}")]
        public async Task<IActionResult> GetRentalById(Guid id)
        {
            try
            {
                var result = await _customerServies.GetRentalById(id);
                if (result == null)
                    return NotFound("Rental not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("rentals/customer/{customerId}")]
        public async Task<IActionResult> GetAllRentalsByCustomerId(Guid customerId)
        {
            try
            {
                var result = await _customerServies.GetAllRentalsByCustomerId(customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var result = await _customerServies.GetAllCustomer();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerRequestDTO customerRequestDTO)
        {
            try
            {
                var result = await _customerServies.Addcustomer(customerRequestDTO);
                return CreatedAtAction(nameof(GetCustomerByID), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}
