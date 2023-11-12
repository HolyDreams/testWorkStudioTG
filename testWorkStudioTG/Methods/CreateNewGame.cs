using Microsoft.AspNetCore.Components.Forms;
using testWorkStudioTG.Models;
using static testWorkStudioTG.Models.FieldValueEnum;

namespace testWorkStudioTG.Methods
{
    public class CreateNewGame
    {
        int _width;
        int _height;
        int _countMines;
        byte[][] _board;
        string[][] _showBoard;
        byte[][] _mines;
        public CreateNewGame(int width, int height, int countMines)
        {
            _width = width;
            _height = height;
            _countMines = countMines;
            _mines = new byte[countMines][];
        }
        public GameStruct GetNewGame()
        {
            createGame();
            return new GameStruct()
            {
                GameID = Guid.NewGuid(),
                FillBoard = _board,
                Field = _showBoard,
                Width = _width,
                Height = _height,
                MinesCount = _countMines,
                Mines = _mines
            };
        }
        private void createGame()
        {
            var board = createNullBoard();
            addMines(board);

            fillBoard(board);
            createShowBoard(board);
        }
        private byte[][] createNullBoard()
        {
            var board = new byte[_height][];
            for (int i = 0; i < _height; i++)
            {
                var row = new byte[_width];
                for (int y = 0; y < _width; y++)
                {
                    row[y] = 1;
                }
                board[i] = row;
            }
            return board;
        }
        private void addMines(byte[][] board)
        {
            var rnd = new Random();
            for (int i = 0; i < _countMines; i++)
            {
                var x = rnd.Next(0, _height);
                var y = rnd.Next(0, _width);
                setMine(x, y, board);
                _mines[i] = new byte[] { (byte)x, (byte)y };
            }
        }
        private void setMine(int x, int y, byte[][] board)
        {
            if (board[x][y] != 10)
            {
                board[x][y] = 10;
                fillArountMide(x, y, board);
            }
            else
            {
                var rnd = new Random();
                var x1 = rnd.Next(0, _height);
                var y1 = rnd.Next(0, _width);
                setMine(x1, y1, board);
            }
        }
        private void fillArountMide(int x, int y, byte[][] board)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= _height)
                    continue;
                for (int q = y - 1; q <= y + 1; q++)
                {
                    if (q < 0 || q >= _width || board[i][q] == 10)
                        continue;

                    board[i][q] += 1;
                }
            }
        }
        private void fillBoard(byte[][] board)
        {
            _board = new byte[board.Length][];
            for (int i = 0; i < board.Length; i++)
            {
                var row = new byte[board[i].Length];
                for (int y = 0; y < board[i].Length; y++)
                {
                    row[y] = board[i][y];
                }
                _board[i] = row;
            }
        }
        private void createShowBoard(byte[][] board)
        {
            _showBoard = new string[board.Length][];
            for (int i = 0; i < board.Length; i++)
            {
                var row = new string[_board[i].Length];
                for (int y = 0; y < board[i].Length; y++)
                {
                    row[y] = new FieldValueEnum(0).Value();
                }
                _showBoard[i] = row;
            }
        }
    }
}
