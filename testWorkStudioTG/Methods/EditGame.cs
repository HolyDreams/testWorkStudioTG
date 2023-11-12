using testWorkStudioTG.Models;
using static testWorkStudioTG.Models.FieldValueEnum;

namespace testWorkStudioTG.Methods
{
    public class EditGame
    {
        GameStruct _game;
        public EditGame(GameStruct game)
        {
            _game = game;
        }
        public bool CheckFill(int x, int y)
        {
            return _game.Field[x][y] != new FieldValueEnum(0).Value();
        }
        public GameStruct AddDot(int x, int y)
        {
            addDot(x, y);
            return _game;
        }

        private void addDot(int x, int y)
        {
            if (_game.FillBoard[x][y] == 10)
            {
                fillCompleteGame(false);
                return;
            }

            fillAround(x, y);
            if (checkComplete())
            {
                fillCompleteGame(true);
            }
            checkMines();
        }
        private void fillAround(int x, int y)
        {
            if (!setPoint(x, y))
                return;

            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= _game.Height)
                    continue;
                for (int q = y - 1; q <= y + 1; q++)
                {
                    if (q < 0 || q >= _game.Width || _game.FillBoard[i][q] == 10 || _game.Field[i][q] != new FieldValueEnum(0).Value())
                        continue;

                    if (setPoint(i, q))
                        fillAround(i, q);
                }
            }
        }
        private bool setPoint(int x, int y)
        {
            var dot = _game.FillBoard[x][y];
            _game.Field[x][y] = new FieldValueEnum((FieldValues)dot).Value();
            return dot == 1;
        }
        private bool checkComplete()
        {
            for (int i = 0; i <  _game.FillBoard.Length; i++)
            {
                for (int y = 0; y < _game.FillBoard[i].Length; y++)
                {
                    if (_game.FillBoard[i][y] != 10 && new FieldValueEnum((FieldValues)_game.FillBoard[i][y]).Value() != _game.Field[i][y])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void fillCompleteGame(bool good)
        {
            for (int i = 0; i < _game.Field.Length; i++)
            {
                for (int y = 0; y < _game.Field[i].Length; y++)
                {
                    int dot = _game.FillBoard[i][y];
                    dot = (!good && dot == 10) ? 11 : dot;
                    _game.Field[i][y] = new FieldValueEnum((FieldValues)dot).Value();
                }
            }
            _game.Completed = true;
        }
        private void checkMines()
        {
            for (int i = 0; i < _game.Mines.Length; i++)
            {
                var x = _game.Mines[i][0];
                var y = _game.Mines[i][1];
                if (checkArountMine(x, y))
                    _game.Field[x][y] = new FieldValueEnum((FieldValues)10).Value();

            }
        }
        private bool checkArountMine(byte x, byte y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= _game.Height)
                    continue;
                for (int q = y - 1; q <= y + 1; q++)
                {
                    if (q < 0 || q >= _game.Width)
                        continue;

                    if (i != x && y != q && _game.Field[x][q] == new FieldValueEnum(0).Value())
                        return false;
                }
            }
            return true;
        }
    }
}
