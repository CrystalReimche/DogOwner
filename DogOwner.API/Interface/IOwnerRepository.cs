using DogOwner.API.Models.Domain;

namespace DogOwner.API.Interface
{
    public interface IOwnerRepository
    {
        //Task<List<Owner>> GetAllOwnersAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
        Task<List<Owner>> GetAllOwnersAsync();
        Task<Owner?> GetOwnerByIdAsync(int id);  // This is nullable because sometimes the id is returned null because owner didn't exist in database
        Task<Owner> CreateOwnerAsync(Owner owner);
        Task<Owner?> UpdateOwnerAsync(int id, Owner owner);    // This is nullable because sometimes the id is returned null because owner didn't exist in database
        Task<Owner?> DeleteOwnerAsync(int id);   // This is nullable because sometimes the id is returned null because owner didn't exist in database
    }
}
