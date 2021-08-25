using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using VideogameStorage.Models;
using VideogameStorage.Services;

namespace VideogameStorage.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class VideogamesController : ControllerBase
    {

        private readonly VideogameService _vService;
        private readonly ILogger<VideogamesController> _logger;

        public VideogamesController(ILogger<VideogamesController> logger, VideogameService vS)
        {
            _logger = logger;
            _vService = vS;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Videogame> PostVideogame([FromBody] Videogame videogame)
        {
            try
            {
                _vService.CreateVideogame(videogame);
            
                return new CreatedResult($"/videogame/{videogame.VideogameId}", videogame);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Unable to POST product.");
            
                return ValidationProblem(e.Message);
            }
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IQueryable<Videogame>> GetVideogames([FromQuery] string gameType,
                                                            [FromQuery] VideogameRequest request)
        {
            // Logger example usage //      //      //      //      //      //
            if (request.Limit >= 100)
                _logger.LogInformation("Requesting more than 100 products.");
            //      //      //      //      //      //      //      //      //

            var result = _vService.GetAllVideogamesByType(gameType);

            Response.Headers["x-total-count"] = result.Count().ToString();
            
            return Ok(result
                .OrderBy(vg => vg.VideogameId)
                .Skip(request.Offset)
                .Take(request.Limit));
        }

        [HttpGet]
        [Route("{videogameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Videogame> GetVideogameById([FromRoute] int videogameId)
        {
            var videogame = _vService.GetVideogameById(videogameId);
            
            if (videogame == null) return NotFound();
            
            return Ok(videogame);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Videogame videogame)
        {
            if (id != videogame.VideogameId)
                return BadRequest();

            var existingVideogame = _vService.GetVideogameById(id);
            if(existingVideogame is null)
                return NotFound();

            _vService.Update(videogame);           

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vg = _vService.GetVideogameById(id);

            if (vg is null)
                return NotFound();

            _vService.Delete(id);

            return NoContent();
        }

    }
}