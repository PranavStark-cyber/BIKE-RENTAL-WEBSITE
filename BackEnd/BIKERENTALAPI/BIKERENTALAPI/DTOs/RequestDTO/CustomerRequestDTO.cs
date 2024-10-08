using BIKERENTALAPI.Entity;

namespace BIKERENTALAPI.DTOs.RequestDTO
{
    public class CustomerRequestDTO
    {
        public string FirstName { get; set; }
        public int Mobilenumber { get; set; }
        public int Licence { get; set; }
        public int Nic { get; set; }
        public string Password { get; set; }
    }
}
