using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WineReviewsApplication.Dto;
using WineReviewsApplication.Interface;
using WineReviewsApplication.Models;
using WineReviewsApplication.Repository;

namespace WineReviewsApplication.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class WineController : Controller
    {
        private readonly IWineRepository _wineRepository;
        private readonly IReviewRepository _reviewrepository;
        private readonly IMapper _mapper;
        public WineController(
            IWineRepository wineRepository,
            IReviewRepository reviewrepository,
            IMapper mapper)
        {
            _wineRepository = wineRepository;
            _reviewrepository = reviewrepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Wine>))]
        public IActionResult GetWine()
        {
            var wines = _mapper.Map<List<WineDto>>(_wineRepository.GetWines());
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(wines);
        }

        [HttpGet("{wineId}")]
        [ProducesResponseType(200, Type = typeof(Wine))]
        [ProducesResponseType(400)]
        public IActionResult GetWine(int wineId) 
        {
            if (!_wineRepository.WineExists(wineId))
                return NotFound();

            var wine = _mapper.Map<WineDto>(_wineRepository.GetWine(wineId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wine);
        }

        [HttpGet("{wineId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetWineRating(int wineId)
        {
            if(!_wineRepository.WineExists(wineId))
                return NotFound();

            var rating = _wineRepository.GetWineRating(wineId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(rating);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWine([FromQuery] int vineyardId, [FromQuery] int wineTypeId, [FromBody] WineDto wineCreate)
        //the wine has to have a category and an owner, you want that data in there.
        {
            if (wineCreate == null)
                return BadRequest(ModelState);

            var wines = _wineRepository.GetWines()
                .Where(c => c.Name.Trim().ToUpper() == wineCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (wines != null)
            {
                ModelState.AddModelError("", "Wine already exists"); //key value
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var wineMap = _mapper.Map<Wine>(wineCreate);

            //wineMap.Country = _countryRepository.GetCountry(countryId); //handling entity framework error

            if (!_wineRepository.CreateWine(vineyardId, wineTypeId, wineMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving."); //key,value - here no key.
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{wineId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateWine(
            int wineId, 
            [FromQuery] int vineyardId, 
            [FromQuery] int vineTypeId, 
            [FromBody] WineDto updatedWine)
        {
            if (updatedWine == null) return BadRequest(ModelState);

            if (wineId != updatedWine.ID) return BadRequest(ModelState);

            if (!_wineRepository.WineExists(wineId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var wineMap = _mapper.Map<Wine>(updatedWine);

            if (!_wineRepository.UpdateWine(vineyardId, vineTypeId, wineMap))
            {
                ModelState.AddModelError("", "Something went wrong updating wine.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{wineId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWine(int wineId)
        {
            if (!_wineRepository.WineExists(wineId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewrepository.GetReviewsOfAWine(wineId);
            var wineToDelete = _wineRepository.GetWine(wineId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_reviewrepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong in the delete wine reviews.");
            }


            if (!_wineRepository.DeleteWine(wineToDelete))
                ModelState.AddModelError("", "Something went wrong in the delete wine function.");

            return NoContent();
        }




    }

    
}
