namespace DogOwner.API.Models.DTO
{
    public class AddDogDto
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }
        public float Weight { get; set; }
        public int OwnerId { get; set; }    
    }
}
