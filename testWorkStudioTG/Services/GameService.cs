﻿using testWorkStudioTG.Methods;
using testWorkStudioTG.Models;

namespace testWorkStudioTG.Services
{
    public class GameService
    {
        private List<GameStruct> GameList = new List<GameStruct>();
        public Guid CreateGame(NewGameRequest gameInfo)
        {
            var createNewGame = new CreateNewGame(gameInfo.Width, gameInfo.Height, gameInfo.MinesCount);
            var newGame = createNewGame.GetNewGame();
            AddGame(newGame);
            return newGame.GameID;
        }
        public void AddGame(GameStruct game)
        {
            GameList.Add(game);
        }
        public GameInfoResponse LoadGame(Guid gameID)
        {
            var game = getGame(gameID);
            return new GameInfoResponse()
            {
                GameID = game.GameID,
                Completed = game.Completed,
                Field = game.Field,
                Height = game.Height,
                MinesCount = game.MinesCount,
                Width = game.Width,
            };
        }
        public bool CheckFill(GameTurnRequest turn)
        {
            var game = getGame(turn.GameID);
            var editor = new EditGame(game);
            return editor.CheckFill(turn.Column, turn.Row);
        }
        public bool CheckField(GameTurnRequest turn)
        {
            var game = getGame(turn.GameID);
            return turn.Column < 0 || turn.Column > game.Height - 1 || turn.Row < 0 || turn.Row > game.Width - 1;
        }
        public void AddDot(GameTurnRequest turn)
        {
            var game = getGame(turn.GameID);
            var editor = new EditGame(game);
            game = editor.AddDot(turn.Column, turn.Row);
        }
        private GameStruct getGame(Guid gameID)
        {
            return GameList.First(a => a.GameID == gameID);
        }
    }
}
