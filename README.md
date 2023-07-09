# DogOwner Api
* A simple ASP.NET Web API service for Dogs and their Owners.
* Swashbuckle is used for display purposes, endpoint testing, sample json data, and simple documentation.
* There are five simple endpoints used for Creating, Reading, Reading All, Updating, and Deleting the Dogs and/or Owners.
* The project has a "Seed.cs" class to populate with dummy data.
* The project uses Entity Framework Core In-Memory database as well as AutoMapper.
* All of the external dependencies are available via Nuget packages.
# Using the API
* This API was built using Visual Studio 2022. When built/ran it will navigate to the Swashbuckle page for a basic documentation of the API. All of the following instructions can also be found on the Swashbuckle page in more detail. Note: the ID within the URL and the "Seed.cs" objects must match.
* It is strongly recommended to use the Swashbuckle documentation page for endpoint testing, and example data sets.
### DOG
  * Creating a new dog. POST https://localhost:7092/api/Dog with a Dog object in the body.
  * Retrieving all dogs. GET https://localhost:7092/api/Dog
  * Retrieving a single dog. GET https://localhost:7092/api/Dog/{id} with the ID of the dog in the url
  * Updating a single dog. PUT https://localhost:7092/api/Dog/{id} with the ID of the dog in the url, and the dog json object in the body.
  * Deleting a single dog. DELETE https://localhost:7092/api/Dog/{id} with the ID of the dog in the url
### Owner
  * Creating a new owner. POST https://localhost:7092/api/Owner with a Dog object in the body.
  * Retrieving all owners. GET https://localhost:7092/api/Owner
  * Retrieving a single owner. GET https://localhost:7092/api/Owner/{id} with the ID of the owner in the url
  * Updating a single owner. PUT https://localhost:7092/api/Owner/{id} with the ID of the owner in the url, and the owner json object in the body.
  * Deleting a single owner. DELETE https://localhost:7092/api/Owner/{id} with the ID of the owner in the url
### JSON
  * A JSON file is created to log changes to the database.
