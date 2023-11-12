using testWorkStudioTG.Models;

namespace testWorkStudioTG.Methods
{
    public class Games
    {
        private static List<GameStruct> GameList = new List<GameStruct>();
        public static Guid CreateGame(NewGameRequest gameInfo)
        {
            var createNewGame = new CreateNewGame(gameInfo.Width, gameInfo.Height, gameInfo.MinesCount);
            var newGame = createNewGame.GetNewGame();
            AddGame(newGame);
            return newGame.GameID;
        }
        public static void AddGame(GameStruct game)
        {
            GameList.Add(game);
        }
        public static GameInfoResponse LoadGame(Guid gameID)
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
        public static bool CheckFill(GameTurnRequest turn)
        {
            var game = getGame(turn.GameID);
            var editor = new EditGame(game);
            return editor.CheckFill(turn.Column, turn.Row);
        }
        public static void AddDot(GameTurnRequest turn)
        {
            var game = getGame(turn.GameID);
            var editor = new EditGame(game);
            game = editor.AddDot(turn.Column, turn.Row);
        }
        private static GameStruct getGame(Guid gameID)
        {
            return GameList.First(a => a.GameID == gameID);
        }
    }
}
