using System.Text.Json.Serialization;

namespace DogOwner.API.Models.Domain
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        public virtual List<Dog> Dogs { get; set; }
    }
}
