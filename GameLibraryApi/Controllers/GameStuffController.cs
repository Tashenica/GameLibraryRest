using GameLibraryApi.Interfaces;
using GameLibraryApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStuffController : ControllerBase
    {

        private IGameService _gameService;

        public GameStuffController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/<GameStuffController>
        [HttpGet]
        public IActionResult GetAllGames()
        {
            try
            {
                List<GameInformation> games = _gameService.GetAllGames();

                if (games == null)
                    return NotFound();

                return Ok(games);
            }
            catch(Exception _)
            {
                return BadRequest();
            }

        }

        // GET: api/<GameStuffController>/gametypes
        [HttpGet("gametypes")]
        public IActionResult GetGameTypes()
        {
            try
            {
                List<GameType> gameTypes = _gameService.GetGameTypes();
                return Ok(gameTypes);
            }
            catch (Exception _)
            {
                return BadRequest();
            }
        }

        // GET: api/<GameStuffController>/genres
        [HttpGet("genres")]
        public IActionResult GetGenres()
        {
            try
            {
                List<Genre> genres = _gameService.GetGenres();
                return Ok(genres);
            }
            catch (Exception _)
            {
                return BadRequest();
            }
        }

        // GET: api/<GameStuffController>/agerestrictions
        [HttpGet("agerestrictions")]
        public IActionResult GetAgeRestrictions()
        {
            try
            {
                List<AgeRestriction> ageRestrictions = _gameService.GetAgeRestrictions();
                return Ok(ageRestrictions);
            }
            catch (Exception _)
            {
                return BadRequest();
            }
        }

        // GET api/<GameStuffController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GameInformation game = _gameService.GetGame(id);

                if (game == null)
                    return NotFound();

                return Ok(game);
            }
            catch(Exception _)
            {
                return BadRequest();
            }

          
        }

        // POST api/<GameStuffController>
        [HttpPost]
        public IActionResult Post([FromBody] GameInformation gameInformation)
        {
            if (gameInformation == null || !ModelState.IsValid)
            {
                return BadRequest("Incorrect Game Information Provided");
            }

            try
            {
                GameInformation gameInfo = _gameService.GetGame(gameInformation.Id);

                if (gameInfo == null)
                {
                    return Ok(_gameService.CreateGame(gameInformation));
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Game Already Exists");

                }
            }
            catch (Exception _)
            {
                return BadRequest();

            }
        }

        // PUT api/<GameStuffController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GameInformation gameInformation)
        {
            if (gameInformation == null || !ModelState.IsValid)
            {
                return BadRequest("Incorrect Game Information Provided");
            }

            try
            {
                GameInformation gameInfo = _gameService.GetGame(gameInformation.Id);

                if (gameInfo == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_gameService.EditGame(gameInformation));

                }
            }
            catch (Exception _)
            {
                return BadRequest();

            }


        }

        // DELETE api/<GameStuffController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                GameInformation gameInfo = _gameService.GetGame(id);

                if (gameInfo == null)
                {
                    return NotFound();
                }
                else
                {
                    _gameService.DeleteGame(id);
                    return Ok();
                }
            }
            catch (Exception _)
            {
                return BadRequest();

            }
        }
    }
}
