using DogOwner.API.Models.Domain;

namespace DogOwner.API.Interface
{
    public interface IDogRepository
    {
        Task<List<Dog>> GetAllDogsAsync(string? filterOn = null, string? filterQuery = null, bool isAscending = true);
        Task<Dog?> GetDogByIdAsync(int id);  // This is nullable because sometimes the id is returned null because dog didn't exist in database
        Task<Dog> CreateDogAsync(Dog dog);
        Task<Dog?> UpdateDogAsync(int id, Dog dog);    // This is nullable because sometimes the id is returned null because dog didn't exist in database
        Task<Dog?> DeleteDogAsync(int id);   // This is nullable because sometimes the id is returned null because dog didn't exist in database
    }
}
