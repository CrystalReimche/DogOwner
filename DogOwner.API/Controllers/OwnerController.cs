using AutoMapper;
using DogOwner.API.Interface;
using DogOwner.API.Models.Domain;
using DogOwner.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DogOwner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        // GET: https://localhost:portnumber/api/owner/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            // Get data from database - Domain Models
            var owner = await _ownerRepository.GetOwnerByIdAsync(id);

            if (owner == null)
            {
                return NotFound("Owner ID not found");
            }

            // Map Domain Model to DTO
            var ownerDto = _mapper.Map<OwnerDto>(owner);

            // Return DTO
            return Ok(ownerDto);
        }

        // GET: https://localhost:portnumber/api/owner
        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            // Get data from database - Domain Models
            var owners = await _ownerRepository.GetAllOwnersAsync();

            // Map Domain Model to DTO
            var ownersDto = _mapper.Map<IList<OwnerDto>>(owners);

            // Return DTO
            return Ok(ownersDto);
        }

        // POST To Create New Owner
        // POST: https://localhost:portnumber/api/owner
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody] AddOwnerDto addOwnerDto)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to Domain Model
                var owner = _mapper.Map<Owner>(addOwnerDto);

                // Use Domain Model to create Owner
                await _ownerRepository.CreateOwnerAsync(owner);

                // Map Domain model back to DTO
                var ownerDto = _mapper.Map<OwnerDto>(owner);

                // Creates a 201 response
                // After sucessful creation, the URL of the newly created object is returned in the response header
                // Since GetOwnerById requires an Id, a new anonymous object is created with the value of ownerDto.Id
                // Also, the new Owner object, along with the new Id is sent back to the client
                return CreatedAtAction(nameof(GetOwnerById), new { id = ownerDto.Id }, ownerDto);
            }
            else
            {
                return BadRequest();
            }
        }

        // Update owner
        // PUT: https://localhost:portnumber/api/owner/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOwner([FromRoute] int id, [FromBody] UpdateOwnerDto updateOwnerDto)
        {
            if (ModelState.IsValid)
            {
                // Map from DTO to Domain model
                var ownerToUpdate = _mapper.Map<Owner>(updateOwnerDto);

                // Check if owner exists
                ownerToUpdate = await _ownerRepository.UpdateOwnerAsync(id, ownerToUpdate);

                if (ownerToUpdate == null)
                {
                    // Produces 404 response
                    return NotFound("Owner ID not found");
                }

                // Convert Domain Model to DTO
                var walkDto = _mapper.Map<OwnerDto>(ownerToUpdate);

                // return updated owner back
                return Ok(walkDto);
            }
            else
            {
                return BadRequest();
            }
        }


        // Delete owner
        // DELETE: https://localhost:portnumber/api/owner/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOwner([FromRoute] int id)
        {
            var ownerToDelete = await _ownerRepository.DeleteOwnerAsync(id);

            // Check if owner exists
            if (ownerToDelete == null)
            {
                // Produces 404 response
                return NotFound("Owner ID not found");
            }

            // map Domain Model to DTO
            var ownerDto = _mapper.Map<OwnerDto>(ownerToDelete);

            // return deleted Walk back
            return Ok(ownerDto);
        }
    }
}
