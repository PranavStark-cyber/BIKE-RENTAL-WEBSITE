namespace BIKERENTALAPI.DTOs.ResponseDTO
{
    public class ManagerResponseDTO
    {
        public Guid ID { get; set; } 
        public string Title { get; set; }
        public string ImageUrl { get; set; } 
        public int Regnumber { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public bool IsAvailable { get; set; } 

    }
}
