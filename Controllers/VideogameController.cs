using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

using VideogameStorage.Models;
using VideogameStorage.Services;
using VideogameStorage.Authentication;


namespace VideogameStorage.Controllers
{
    //[Authorize]
    [ApiController]
    [EnableCors("LocalHostPolicy")]
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

        [HttpGet]
        [ProducesResponseType(typeof(List<Videogame>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllVideogames(int offset, int limit)
        {
            try
            {
                var videogames = await _vService.GetAllVideogamesAsync();
                return Ok(videogames);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("{offset}/{limit}")]
        [ProducesResponseType(typeof(List<Videogame>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVideogames(int offset, int limit)
        {
            try
            {
                var videogames = await _vService.GetVideogamesAsync(offset, limit);
                Response.Headers.Add("X-Inline-Count", videogames.TotalRecords.ToString());
                return Ok(videogames.Records);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("{videogameId}", Name = "GetVideogameRoute")]
        [ProducesResponseType(typeof(Videogame), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetVideogameById(int videogameId)
        {
            try
            {
                var videogame = await _vService.GetVideogameAsync(videogameId);
                return Ok(videogame);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest(exp.Message);
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostVideogame([FromBody] Videogame videogame)
        {
            try
            {
                var newVideogame = await _vService.CreateVideogameAsync(videogame);
                if (newVideogame == null)
                {
                    return BadRequest();
                }
                return CreatedAtRoute("GetVideogameRoute", new { videogameId = newVideogame.VideogameId }, newVideogame);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest();
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{videogameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutVideogameById(int videogameId, [FromBody] Videogame videogame)
        {
            try
            {
                if (videogameId != videogame.VideogameId)
                    return NotFound();

                var status = await _vService.UpdateVideogameAsync(videogame);
                if (!status)
                {
                    return BadRequest();
                }
            
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to PUT videogame.");
                return ValidationProblem(e.Message);
            }
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{videogameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVideogame(int videogameId)
        {
            try
            {
                var status = await _vService.DeleteVideogameAsync(videogameId);
                if (!status)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest();
            }
        }
        
    }
}