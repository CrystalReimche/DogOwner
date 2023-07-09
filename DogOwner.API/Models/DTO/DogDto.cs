using DogOwner.API.Models.Domain;

namespace DogOwner.API.Models.DTO
{
    public class DogDto : AddDogDto
    {
        public int Id { get; set; }
    }
}
