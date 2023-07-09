using DogOwner.API.Data;
using DogOwner.API.Interface;
using DogOwner.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DogOwner.API.Repository
{
    public class DogRepository : IDogRepository
    {
        private readonly DogOwnerContext _dbContext;

        public DogRepository(DogOwnerContext dbContext)
        {
            _dbContext = dbContext;
        }


        private void UpdateJsonStore(string method)
        {
            string json = JsonConvert.SerializeObject(_dbContext.Dogs.ToList(), Formatting.Indented);
            File.WriteAllText($@"dogs{method}.json", json);
        }

        public async Task<Dog> CreateDogAsync(Dog dog)
        {
            await _dbContext.Dogs.AddAsync(dog);
            await _dbContext.SaveChangesAsync();
            UpdateJsonStore("Created");
            return dog;
        }

        public async Task<Dog?> DeleteDogAsync(int id)
        {
            var dogToDelete = await _dbContext.Dogs.FirstOrDefaultAsync(x => x.Id == id);

            if (dogToDelete == null)
            {
                return null;
            }
            else
            {
                _dbContext.Dogs.Remove(dogToDelete);
                await _dbContext.SaveChangesAsync();

                return dogToDelete;
            }
        }

        public async Task<List<Dog>> GetAllDogsAsync(string? filterOn = null, string? filterQuery = null, bool isAscending = true)
        {
            var dogs = _dbContext.Dogs.Include(o => o.Owner).AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("OwnerId", StringComparison.OrdinalIgnoreCase))
                {
                    dogs = dogs.Where(x => x.OwnerId.ToString() == filterQuery);
                }
            }

            // Sorting
            dogs = isAscending ? dogs.OrderBy(x => x.Id) : dogs.OrderByDescending(x => x.Id);
            
            return await dogs.ToListAsync();
        }

        public async Task<Dog?> GetDogByIdAsync(int id)
        {
            var dog = await _dbContext.Dogs
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dog == null)
            {
                return null;
            }
            else
            {
                return dog;
            }
        }

        public async Task<Dog?> UpdateDogAsync(int id, Dog dog)
        {
            var dogToUpdate = await _dbContext.Dogs.FirstOrDefaultAsync(x => x.Id == id);

            if (dogToUpdate == null)
            {
                return null;
            }
            else
            {
                dogToUpdate.Name = dog.Name;
                dogToUpdate.Breed = dog.Breed;
                dogToUpdate.Age = dog.Age;
                dogToUpdate.Gender = dog.Gender;
                dogToUpdate.Color = dog.Color;
                dogToUpdate.Weight = dog.Weight;
                dogToUpdate.OwnerId = dog.OwnerId;

                await _dbContext.SaveChangesAsync();

                UpdateJsonStore("Updated");

                return dogToUpdate;
            }
        }
    }
}
