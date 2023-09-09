using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.loggingh;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<VillaAPIController> _logger;
        private readonly ILogging log;

        public VillaAPIController(ILogger<VillaAPIController> logger,ILogging log, ApplicationDbContext db)
        {
            _logger = logger;
            this.log = log;
            _db = db;   
        }


        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            log.Log("Getting all villas", "info");
            //_logger.LogInformation("Getting all villas");

            return Ok(_db.Villas.ToList()); //

        }                        

        
        [HttpGet("{id:int}", Name = "GetVilla")]
        //[producesresponsetype(200, type = typeof(villadto))]
        //[producesresponsetype(404)]
        //[producesresponsetype(400)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVailla(int id)
        {
            //_logger.LogInformation("Getting individual villas");
            log.Log("Getting Individual Villas", "Info");
            if (id == 0)
            {
                //_logger.LogError("Getting villa with Id " + id);
                log.Log("Error getting villa with Id " + id, "error");
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound("Id "+ id+ " Not found");
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateDto([FromBody] VillaDto villaDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower())!=null) {
                ModelState.AddModelError("CustomError", "Villa already Exist"); // this is how we add custom validation to our API
                return BadRequest(ModelState);

            }
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if(villaDto.Id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //villaDto.Id = _db.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            //VillaStore.villaList.Add(villaDto)/*;*/
            var model = new Villa()
            {
                Amenity = villaDto.Amenity,
                Name = villaDto.Name,
                Sqft = villaDto.Sqft,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,

            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new {id=villaDto.Id},villaDto);


        }


        [HttpDelete("{id:int}", Name = "DeletVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}",Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if(villaDto == null || id !=villaDto.Id) {
                return BadRequest();
            }
            //var villa = _d.FirstOrDefault(u => u.Id == id);
            //villa.Name = villaDto.Name;
            //villa.Occupancy = villaDto.Occupancy;
            //villa.Sqft = villaDto.Sqft;
            Villa model = new()
            {
                Amenity = villaDto.Amenity,
                Name = villaDto.Name,
                Rate = villaDto.Rate,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Occupancy = villaDto.Occupancy,
                Sqft = villaDto.Sqft,


            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();

        }


        // Patch is used to update only one property
        //[HttpPatch]
    }
}
