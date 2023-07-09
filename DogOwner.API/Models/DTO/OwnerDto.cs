namespace DogOwner.API.Models.DTO
{
    public class OwnerDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DogDto> Dogs { get; set; }
    }
}
