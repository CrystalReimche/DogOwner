using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace DogOwner.API.Models.Domain
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }
        public float Weight { get; set; }



        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }

        // Navigation property
        public Owner Owner { get; set; }
    }
}
