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
            catch(Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch(Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return BadRequest();

            }
        }

        // GET api/<GameStuffController>/5/image
        [HttpGet("{id}/image")]
        public IActionResult GetGameImage(int id)
        {
            try
            {
                GameInformation game = _gameService.GetGame(id);

                if (game == null)
                    return NotFound("Game not found");

                if (game.ImageData == null || game.ImageData.Length == 0)
                    return NotFound("No image found for this game");

                return File(game.ImageData, game.ImageContentType, game.ImageFileName);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST api/<GameStuffController>/5/image
        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadGameImage(int id, IFormFile image)
        {
            try
            {
                GameInformation game = _gameService.GetGame(id);

                if (game == null)
                    return NotFound("Game not found");

                if (image == null || image.Length == 0)
                    return BadRequest("No image provided");

                var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp" };
                if (!allowedTypes.Contains(image.ContentType.ToLower()))
                    return BadRequest("Invalid image type. Allowed types: JPEG, PNG, GIF, BMP");

                if (image.Length > 5 * 1024 * 1024)
                    return BadRequest("Image size cannot exceed 5MB");

                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    game.ImageData = memoryStream.ToArray();
                    game.ImageFileName = image.FileName;
                    game.ImageContentType = image.ContentType;
                }

                _gameService.EditGame(game);

                return Ok(new { message = "Image uploaded successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<GameStuffController>/5/image
        [HttpDelete("{id}/image")]
        public IActionResult DeleteGameImage(int id)
        {
            try
            {
                GameInformation game = _gameService.GetGame(id);

                if (game == null)
                    return NotFound("Game not found");

                if (game.ImageData == null || game.ImageData.Length == 0)
                    return NotFound("No image found for this game");

                // Clear image data
                game.ImageData = null;
                game.ImageFileName = null;
                game.ImageContentType = null;

                // Update the game
                _gameService.EditGame(game);

                return Ok(new { message = "Image deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
