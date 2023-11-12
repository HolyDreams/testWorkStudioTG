using Microsoft.AspNetCore.Mvc;
using testWorkStudioTG.Methods;
using testWorkStudioTG.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testWorkStudioTG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Saper : ControllerBase
    {
        // POST api/<Saper>
        [HttpPost]
        [Route("new")]
        public async Task<ActionResult<GameInfoResponse>> NewGame([FromBody] NewGameRequest newGame)
        {
            try
            {
                if (newGame.MinesCount > newGame.Width * newGame.Height)
                    throw new Exception("Количество мин превышает количество ячеек!");

                var gameID = Games.CreateGame(newGame);
                return Ok(Games.LoadGame(gameID));
            }
            catch
            {
                return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpPost]
        [Route("turn")]
        public async Task<ActionResult<GameInfoResponse>> Turn([FromBody] GameTurnRequest turn)
        {
            try
            {
                if (Games.CheckFill(turn))
                    return BadRequest("Данная ячейка уже заполнена!");
                if (Games.CheckField(turn))
                    return BadRequest("Попытка уйти за границы поля!");

                Games.AddDot(turn);
                var game = Games.LoadGame(turn.GameID);

                return Ok(Games.LoadGame(turn.GameID));
            }
            catch
            {
                return BadRequest("Неизвестная ошибка");
            }
        }
    }
}