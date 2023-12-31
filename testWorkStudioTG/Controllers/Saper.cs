﻿using Microsoft.AspNetCore.Mvc;
using testWorkStudioTG.Methods;
using testWorkStudioTG.Models;
using testWorkStudioTG.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testWorkStudioTG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Saper : ControllerBase
    {
        private readonly GameService _game;
        public Saper(GameService game) 
        { 
            _game = game;
        }
        // POST api/<Saper>
        [HttpPost]
        [Route("new")]
        public async Task<ActionResult<GameInfoResponse>> NewGame([FromBody] NewGameRequest newGame)
        {
            try
            {
                if (newGame.Width <= 0 || newGame.Height <= 0)
                    return BadRequest("Попытка создать поле с 0 ячеек!");
                else if (newGame.MinesCount > newGame.Width * newGame.Height - 1)
                    return BadRequest("Количество мин превышает количество ячеек!");
                else if (newGame.Width > 30 || newGame.Height > 30)
                    return BadRequest("Превышен размер поля!");

                var gameID = _game.CreateGame(newGame);
                return Ok(_game.LoadGame(gameID));
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
                if (_game.CheckFill(turn))
                    return BadRequest("Данная ячейка уже заполнена!");
                if (_game.CheckField(turn))
                    return BadRequest("Попытка уйти за границы поля!");

                _game.AddDot(turn);
                var game = _game.LoadGame(turn.GameID);
                var res = "";
                for (int i = 0; i < game.Height; i++)
                {
                    res += string.Join(",", game.Field[i]) + Environment.NewLine;
                }
                return Ok(_game.LoadGame(turn.GameID));
            }
            catch
            {
                return BadRequest("Неизвестная ошибка");
            }
        }
    }
}