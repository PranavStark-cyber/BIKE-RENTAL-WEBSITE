namespace BIKERENTALAPI.Entity
{
    public class Rental
    {
        public Guid id {  get; set; }
        public Guid CustomerID { get; set; }
        public Guid MotorbikeID { get; set; }
        public DateTime RentalDate {  get; set; }
        public DateTime? Returndate { get; set; }
        public bool Isoverdue { get; set; } = false;
        public string status { get; set; } = "Pending";
        public Customer Customer { get; set; }
        public Bikes Motorbike { get; set; }
    }
}
