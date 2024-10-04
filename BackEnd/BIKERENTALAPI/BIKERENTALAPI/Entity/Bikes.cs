namespace BIKERENTALAPI.Entity
{
    public class Bikes
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; } 
        public int Regnumber { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public bool IsAvailable { get; set; }



    }
}
