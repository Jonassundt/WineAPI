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
    public class WineTypeController : Controller
    {
        private readonly IWineTypeRepository _wineTypeRepository;
        private readonly IMapper _mapper;

        public WineTypeController(IWineTypeRepository wineTypeRepository, IMapper mapper)
        {
            _wineTypeRepository = wineTypeRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WineType>))]
        public IActionResult GetWineTypes()
        {
            var wineTypes = _mapper.Map<List<WineTypeDto>>(_wineTypeRepository.GetWineTypes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(wineTypes);
        }

        [HttpGet("{wineTypeId}")]
        [ProducesResponseType(200, Type = typeof(WineType))]
        [ProducesResponseType(400)]
        public IActionResult GetWineType(int wineTypeId)
        {
            if (!_wineTypeRepository.WineTypeExists(wineTypeId))
                return NotFound();

            var wineType = _mapper.Map<WineTypeDto>(_wineTypeRepository.GetWineType(wineTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wineType);
        }

        [HttpGet("wine/{wineTypeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Wine>))]
        [ProducesResponseType(400)]
        public IActionResult GetWineByWineTypeId(int wineTypeId)
        {
            var wines = _mapper.Map<List<WineDto>>(_wineTypeRepository.GetWineByWineType(wineTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wines);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWineType([FromBody] WineTypeDto wineTypeCreate)
        {
            if (wineTypeCreate == null)
                return BadRequest(ModelState);

            var wineType = _wineTypeRepository.GetWineTypes()
                .Where(c => c.Type.Trim().ToUpper() == wineTypeCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(wineType != null)
            {
                ModelState.AddModelError("", "WineType already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var wineTypeMap = _mapper.Map<WineType>(wineTypeCreate);

            if(!_wineTypeRepository.CreateWineType(wineTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving."); //key,value - here no key.
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{wineTypeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateWineType(int wineType, [FromBody] WineTypeDto updatedWineType) 
        {
            if (updatedWineType == null) return BadRequest(ModelState);

            if (wineType != updatedWineType.Id) return BadRequest(ModelState);

            if (!_wineTypeRepository.WineTypeExists(wineType)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var wineTypeMap = _mapper.Map<WineType>(updatedWineType);

            if(!_wineTypeRepository.UpdateWineType(wineTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating wineType.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        
        }

        [HttpDelete("{wineTypeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWineType(int wineTypeId)
        {
            if (!_wineTypeRepository.WineTypeExists(wineTypeId))
            {
                return NotFound();
            }

            var wineTypeToDelete = _wineTypeRepository.GetWineType(wineTypeId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_wineTypeRepository.DeleteWineType(wineTypeToDelete))
                ModelState.AddModelError("", "Something went wrong in the delete wineType.");

            return NoContent();
        }
    }
}
