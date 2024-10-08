using BIKERENTALAPI.Entity;

namespace BIKERENTALAPI.DTOs.RequestDTO
{
    public class RentalRequestDTO
    {
        public Guid CustomerID { get; set; }
        public Guid MotorbikeID { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? Returndate { get; set; }

    }
}
