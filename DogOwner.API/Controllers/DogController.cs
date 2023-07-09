using AutoMapper;
using DogOwner.API.Interface;
using DogOwner.API.Models.Domain;
using DogOwner.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DogOwner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;

        public DogController(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }

        // GET: https://localhost:portnumber/api/dog/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDogById(int id)
        {
            // Get data from database - Domain Models
            var dog = await _dogRepository.GetDogByIdAsync(id);

            if (dog == null)
            {
                return NotFound("Dog ID not found");
            }

            // Map Domain Model to DTO
            var dogDto = _mapper.Map<DogDto>(dog);

            // Return DTO
            return Ok(dogDto);
        }

        // GET: https://localhost:portnumber/api/dog
        [HttpGet]
        public async Task<IActionResult> GetAllDogs([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] bool? isAscending)
        {
            // Get data from database - Domain Models
            var dogs = await _dogRepository.GetAllDogsAsync(filterOn, filterQuery, isAscending ?? true);

            // Map Domain Model to DTO
            var dogsDto = _mapper.Map<IList<DogDto>>(dogs);

            // Return DTO
            return Ok(dogsDto);
        }

        // POST To Create New Dog
        // POST: https://localhost:portnumber/api/dog
        [HttpPost]
        public async Task<IActionResult> CreateDog([FromBody] AddDogDto addDogDto)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to Domain Model
                var dog = _mapper.Map<Dog>(addDogDto);

                // Use Domain Model to create Dog
                await _dogRepository.CreateDogAsync(dog);

                // Map Domain model back to DTO
                var dogDto = _mapper.Map<DogDto>(dog);

                // Creates a 201 response
                // After sucessful creation, the URL of the newly created object is returned in the response header
                // Since GetDogById requires an Id, a new anonymous object is created with the value of dogDto.Id
                // Also, the new Dog object, along with the new Id is sent back to the client
                return CreatedAtAction(nameof(GetDogById), new { id = dogDto.Id }, dogDto);
            }
            else
            {
                return BadRequest();
            }
        }

        // Update dog
        // PUT: https://localhost:portnumber/api/dog/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDog([FromRoute] int id, [FromBody] UpdateDogDto updateDogDto)
        {
            if (ModelState.IsValid)
            {
                // Map from DTO to Domain model
                var dogToUpdate = _mapper.Map<Dog>(updateDogDto);

                // Check if dog exists
                dogToUpdate = await _dogRepository.UpdateDogAsync(id, dogToUpdate);

                if (dogToUpdate == null)
                {
                    // Produces 404 response
                    return NotFound("Dog ID not found");
                }

                // Convert Domain Model to DTO
                var dogDto = _mapper.Map<DogDto>(dogToUpdate);

                // return updated dog back
                return Ok(dogDto);
            }
            else
            {
                return BadRequest();
            }
        }

        // Delete dog
        // DELETE: https://localhost:portnumber/api/dog/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDog([FromRoute] int id)
        {
            var dogToDelete = await _dogRepository.DeleteDogAsync(id);

            // Check if dog exists
            if (dogToDelete == null)
            {
                // Produces 404 response
                return NotFound("Dog ID not found");
            }

            // map Domain Model to DTO
            var dogDto = _mapper.Map<DogDto>(dogToDelete);

            // return deleted Dog back
            return Ok(dogDto);
        }
    }
}
