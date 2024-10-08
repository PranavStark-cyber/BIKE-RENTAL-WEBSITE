namespace BIKERENTALAPI.Entity
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public int Mobilenumber { get; set; }
        public int Licence { get; set; }
        public int Nic { get; set; }
        public string Password { get; set; }
        public ICollection<Rental> Rental_history { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
