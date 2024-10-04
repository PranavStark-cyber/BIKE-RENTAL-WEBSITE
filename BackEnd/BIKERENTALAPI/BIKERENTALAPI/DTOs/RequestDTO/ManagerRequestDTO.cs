namespace BIKERENTALAPI.DTOs.RequestDTO
{
    public class ManagerRequestDTO
    {
        public string Title { get; set; }
        public int Regnumber { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
