using BIKERENTALAPI.Entity;

namespace BIKERENTALAPI.DTOs.ResponseDTO
{
    public class RentalResponseDTO
    {
        public Guid id { get; set; }
        public Guid CustomerID { get; set; }
        public Guid MotorbikeID { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? Returndate { get; set; }
        public bool Isoverdue { get; set; }
        public bool Paymentstatus { get; set; }
        public string status { get; set; }
        public Customer Customer { get; set; }
        public Bikes Motorbike { get; set; }
    }
}
