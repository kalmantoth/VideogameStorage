using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
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
                _vService.Create(videogame);
            
                return new CreatedResult($"/videogame/{videogame.VideogameId}", videogame);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Unable to POST videogame.");
            
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
                _logger.LogInformation("Requesting more than 100 videogames.");
            //      //      //      //      //      //      //      //      //

            var result = _vService.GetAll(gameType);

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
            var videogameDb = _vService.Get(videogameId);
            
            if (videogameDb == null) return NotFound();
            
            return Ok(videogameDb);
        }


        [HttpPut("{videogameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Videogame> PutVideogameById([FromRoute] int videogameId, [FromBody] Videogame videogame)
        {
            try
            {
                if (videogameId != videogame.VideogameId)
                    return BadRequest();

                var videogameDb = _vService.Get(videogameId);

                if (videogameDb == null) return NotFound();
            
                _vService.Update(videogame);
            
                return Ok(videogame);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Unable to PUT videogame.");
            
                return ValidationProblem(e.Message);
            }
        }

        [HttpDelete]
        [Route("{videogameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Videogame> DeleteVideogame([FromRoute] int videogameId)
        {
            var videogameDb = _vService.Get(videogameId);

            if (videogameDb == null) return NotFound();
            
            _vService.Delete(videogameId);
            
            return NoContent();
        }

        [HttpPatch]
        [Route("{videogameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Videogame> PatchProduct([FromRoute] 
            int videogameId, [FromBody] JsonPatchDocument<Videogame> patch)
        {
            try
            {
                var videogameDb = _vService.Get(videogameId);
            
                if (videogameDb == null) return NotFound();

                patch.ApplyTo(videogameDb, ModelState);
                if (!ModelState.IsValid || !TryValidateModel(videogameDb)) 
                        return ValidationProblem(ModelState);
                _vService.SaveDBChanges();
                
                _vService.UpdatePartially(videogameId, patch);
                
                return Ok(videogameDb);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Unable to PATCH videogame.");
            
                return ValidationProblem(e.Message);
            }
        }

    }
}