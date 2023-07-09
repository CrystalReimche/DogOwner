using DogOwner.API.Models.Domain;

namespace DogOwner.API.Data
{
    public class Seed
    {
        // Seeding InMemoryDatabase
        public static void SeedAsync(DogOwnerContext dbContext)
        {
            if (!dbContext.Owners.Any())
            {
                var owners = new List<Owner>
                {
                    new Owner
                    {
                        Id = 1,
                        Name = "Crystal"
                    },
                    new Owner
                    {
                        Id = 2,
                        Name = "Jaja"
                    },
                    new Owner
                    {
                        Id = 3,
                        Name = "Nona"
                    }
                };

                dbContext.Owners.AddRange(owners);
                dbContext.SaveChanges();
            }


            if (!dbContext.Dogs.Any())
            {
                var dogs = new List<Dog>
                {
                    new Dog
                    {
                        Id = 1,
                        Name = "Ragnar",
                        Breed = "Great Dane",
                        Age = 7,
                        Gender = "Male",
                        Color = "Fawn",
                        Weight = 175,
                        OwnerId = 1
                    },
                    new Dog
                    {
                        Id = 2,
                        Name = "Aspen",
                        Breed = "Great Dane",
                        Age = 4,
                        Gender = "Female",
                        Color = "Blanketed Blue Harlequin",
                        Weight = 125,
                        OwnerId = 1
                    },
                    new Dog
                    {
                        Id = 3,
                        Name = "Piper",
                        Breed = "Terrier Mix",
                        Age = 11,
                        Gender = "Female",
                        Color = "Onyx Brindle",
                        Weight = 30,
                        OwnerId = 2
                    },
                    new Dog
                    {
                        Id = 4,
                        Name = "Boots",
                        Breed = "Chihuahua",
                        Age = 12,
                        Gender = "Male",
                        Color = "Fawn",
                        Weight = 10,
                        OwnerId = 3
                    }
                };

                dbContext.Dogs.AddRange(dogs);
                dbContext.SaveChanges();
            }
        }
    }
}
