using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WineReviewsApplication.Dto;
using WineReviewsApplication.Interface;
using WineReviewsApplication.Models;
using WineReviewsApplication.Repository;

namespace WineReviewsApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VineyardController : Controller
    {
        private readonly IVineyardRepository _vineyardRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public VineyardController(IVineyardRepository vineyardRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _vineyardRepository = vineyardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vineyard>))]
        public IActionResult GetVineyards()
        {
            var vineyards = _mapper.Map<List<VineyardDto>>(_vineyardRepository.GetVineyards());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vineyards);
        }

        [HttpGet("{vineyardId}")]
        [ProducesResponseType(200, Type = typeof(Vineyard))]
        [ProducesResponseType(400)]
        public IActionResult GetVineyard(int vineyardId)
        {
            if (!_vineyardRepository.VineyardExists(vineyardId))
                return NotFound();

            var vineyard = _mapper.Map<VineyardDto>(_vineyardRepository.GetVineyard(vineyardId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(vineyard);
        }

        [HttpGet("{vineyardId}/wine")]
        [ProducesResponseType(200, Type=typeof(Vineyard))]
        [ProducesResponseType(400)]
        public IActionResult GetWineByVineyard(int vineyardId)
        {
            if(!_vineyardRepository.VineyardExists(vineyardId))
            {
                return NotFound();
            }

            var vineyard = _mapper.Map<List<WineDto>>(_vineyardRepository.GetWineByVineyard(vineyardId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(vineyard);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateVineyard([FromQuery] int countryId, [FromBody] VineyardDto vineyardCreate)
        {
            if (vineyardCreate == null)
                return BadRequest(ModelState);

            var vineyard = _vineyardRepository.GetVineyards() //firstname and lastname
                .Where(c => c.Name.Trim().ToUpper() == vineyardCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (vineyard != null)
            {
                ModelState.AddModelError("", "Vineyard already exists"); //key value
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vineyardMap = _mapper.Map<Vineyard>(vineyardCreate); //automapper is also good for converting types quickly

            vineyardMap.Country = _countryRepository.GetCountry(countryId); //handling entity framework error

            if (!_vineyardRepository.CreateVineyard(vineyardMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving."); //key,value - here no key.
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{vineyardId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateVineyard(int vineyardId, [FromBody] VineyardDto updatedVineyard)
        {
            if (updatedVineyard == null) return BadRequest(ModelState);

            if (vineyardId != updatedVineyard.Id) return BadRequest(ModelState);

            if (!_vineyardRepository.VineyardExists(vineyardId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var vineyardMap = _mapper.Map<Vineyard>(updatedVineyard);

            if (!_vineyardRepository.UpdateVineyard(vineyardMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{vineyardId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVineyard(int vineyardId)
        {
            if (!_vineyardRepository.VineyardExists(vineyardId))
            {
                return NotFound();
            }

            var vineyardToDelete = _vineyardRepository.GetVineyard(vineyardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_vineyardRepository.DeleteVineyard(vineyardToDelete))
                ModelState.AddModelError("", "Something went wrong in the delete vineyard.");

            return NoContent();
        }




    }
}
