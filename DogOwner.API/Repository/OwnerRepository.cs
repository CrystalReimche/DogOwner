using DogOwner.API.Data;
using DogOwner.API.Interface;
using DogOwner.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DogOwner.API.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DogOwnerContext _dbContext;

        public OwnerRepository(DogOwnerContext dbContext)
        {
            _dbContext = dbContext;
        }

        private void UpdateJsonStore(string method)
        {
            string json = JsonConvert.SerializeObject(_dbContext.Owners.ToList(), Formatting.Indented);
            File.WriteAllText($@"owners{method}.json", json);
        }

        public async Task<Owner> CreateOwnerAsync(Owner owner)
        {
            await _dbContext.Owners.AddAsync(owner);
            await _dbContext.SaveChangesAsync();

            UpdateJsonStore("Created");

            return owner;
        }

        public async Task<Owner?> DeleteOwnerAsync(int id)
        {
            var ownerToDelete = await _dbContext.Owners.FirstOrDefaultAsync(x => x.Id == id);

            if (ownerToDelete == null)
            {
                return null;
            }
            else
            {
                _dbContext.Owners.Remove(ownerToDelete);
                await _dbContext.SaveChangesAsync();

                return ownerToDelete;
            }
        }

        public async Task<List<Owner>> GetAllOwnersAsync()
        {
            var owners = await _dbContext.Owners.Include(d => d.Dogs).ToListAsync();

            return owners;
        }

        public async Task<Owner?> GetOwnerByIdAsync(int id)
        {
            var owner = await _dbContext.Owners
                .Include(d => d.Dogs)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (owner == null)
            {
                return null;
            }
            else
            {
                return owner;
            }
        }

        public async Task<Owner?> UpdateOwnerAsync(int id, Owner owner)
        {
            var ownerToUpdate = await _dbContext.Owners.FirstOrDefaultAsync(x => x.Id == id);

            if (ownerToUpdate == null)
            {
                return null;
            }
            else
            {
                ownerToUpdate.Name = owner.Name;

                await _dbContext.SaveChangesAsync();

                UpdateJsonStore("Updated");

                return ownerToUpdate;
            }
        }
    }
}
