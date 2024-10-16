
using BIKERENTALAPI.DTOs.RequestDTO;
using BIKERENTALAPI.DTOs.ResponseDTO;
using BIKERENTALAPI.Entity;
using BIKERENTALAPI.IRepository;
using BIKERENTALAPI.IServies;

namespace BIKERENTALAPI.Servies
{
        public class ManagerService : IManagerServies
        {
            private readonly IManagerRepository _managerRepository;
            private readonly IWebHostEnvironment _webHostEnvironment;

            public ManagerService(IManagerRepository managerRepository, IWebHostEnvironment webHostEnvironment)
            {
                _managerRepository = managerRepository;
                _webHostEnvironment = webHostEnvironment;
            }

        public async Task<ManagerResponseDTO> AddBikeAsync(ManagerRequestDTO bikeRequest)
        {
            var bike = new Bikes
            {
                ID = Guid.NewGuid(),
                Title = bikeRequest.Title,
                Regnumber = bikeRequest.Regnumber,
                Brand = bikeRequest.Brand,
                Category = bikeRequest.Category,
                Description = bikeRequest.Description,
                Model = bikeRequest.Model,
            };

            
            if (bikeRequest.ImageFile != null && bikeRequest.ImageFile.Length > 0)
            {
                
                if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
                {
                    throw new ArgumentNullException(nameof(_webHostEnvironment.WebRootPath), "WebRootPath is not set. Make sure the environment is configured properly.");
                }

                var bikeImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "bikeimages");

                if (!Directory.Exists(bikeImagesPath))
                {
                    Directory.CreateDirectory(bikeImagesPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(bikeRequest.ImageFile.FileName);
                var imagePath = Path.Combine(bikeImagesPath, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await bikeRequest.ImageFile.CopyToAsync(stream);
                }

                bike.ImagePath = "/bikeimages/" + fileName; 
            }

            var addedBike = await _managerRepository.AddBike(bike);

            return new ManagerResponseDTO
            {
                ID = addedBike.ID,
                Title = addedBike.Title,
                ImageUrl = addedBike.ImagePath,
                Regnumber = addedBike.Regnumber,
                Brand = addedBike.Brand,
                Category = addedBike.Category,
                Description = addedBike.Description,
                Model = addedBike.Model,
                IsAvailable = addedBike.IsAvailable,
            };
        }

        public async Task<ManagerResponseDTO> GetBikeByIdAsync(Guid bikeId)
            {
                // Get the bike from the repository
                var bike = await _managerRepository.GetBikeById(bikeId);
                if (bike == null) return null;

                // Return the response DTO
                return new ManagerResponseDTO
                {
                    ID = bike.ID,
                    Title = bike.Title,
                    ImageUrl = bike.ImagePath,
                    Regnumber = bike.Regnumber,
                    Brand = bike.Brand,
                    Category = bike.Category,
                    Description = bike.Description,
                    Model = bike.Model,
                    IsAvailable = bike.IsAvailable,
                };
            }

            public async Task<List<ManagerResponseDTO>> GetAllBikesAsync()
            {
               
                var bikes = await _managerRepository.GetAllBikes();

          
                return bikes.Select(b => new ManagerResponseDTO
                {
                    ID = b.ID,
                    Title = b.Title,
                    ImageUrl = b.ImagePath,
                    Regnumber = b.Regnumber,
                    Brand = b.Brand,
                    Category = b.Category,
                    Description = b.Description,
                    Model = b.Model,
                    IsAvailable = b.IsAvailable,
                }).ToList();
            }

            public async Task<ManagerResponseDTO> EditBikeAsync(Guid bikeId, ManagerRequestDTO bikeRequest)
            {
                // Get the bike by ID
                var bike = await _managerRepository.GetBikeById(bikeId);
                if (bike == null) return null;

                // Update the bike's basic information
                bike.Title = bikeRequest.Title;
                bike.Regnumber = bikeRequest.Regnumber;
                bike.Brand = bikeRequest.Brand;
                bike.Category = bikeRequest.Category;
                bike.Description = bikeRequest.Description;
                bike.Model = bikeRequest.Model;

                // Handle the image upload if a new image is provided
                if (bikeRequest.ImageFile != null && bikeRequest.ImageFile.Length > 0)
                {
                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(bike.ImagePath))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, bike.ImagePath.TrimStart('/'));
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }

                    // Save the new image
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(bikeRequest.ImageFile.FileName);
                    var newImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "bikeimages", fileName);

                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await bikeRequest.ImageFile.CopyToAsync(stream);
                    }

                    // Update the image path
                    bike.ImagePath = "/bikeimages/" + fileName;
                }

                // Update the bike in the database
                await _managerRepository.EditBike(bike);

                // Return the updated bike as a response DTO
                return new ManagerResponseDTO
                {
                    ID = bike.ID,
                    Title = bike.Title,
                    ImageUrl = bike.ImagePath,
                    Regnumber = bike.Regnumber,
                    Brand = bike.Brand,
                    Category = bike.Category,
                    Description = bike.Description,
                    Model = bike.Model,
                    IsAvailable = bike.IsAvailable,
                };
            }

            public async Task<bool> DeleteBikeAsync(Guid bikeId)
            {
                // Find the bike by ID
                var bike = await _managerRepository.GetBikeById(bikeId);
                if (bike == null) return false;

                // Delete the image from the server if it exists
                if (!string.IsNullOrEmpty(bike.ImagePath))
                {
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, bike.ImagePath.TrimStart('/'));
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }

                // Delete the bike from the database using the repository
                await _managerRepository.DeleteBike(bikeId);
                return true;
            }

    }
    
}
