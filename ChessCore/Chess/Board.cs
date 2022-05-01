using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore
{
    internal class Board
    {
        public string Fen { get;private set; }
        Figure[,] _figures;
        public Color MoveColor { get; private set; }
        public int MoveNum { get; private set; }
        public Board(string Fen)
        {
            this.Fen = Fen;
            _figures = new Figure[8,8];
            Init();
        }

        private void Init()
        {
            //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            string[] parts = Fen.Split();
            if (parts.Length != 6)
            {               
                return;
            }
            InitFirures(parts[0]);
            MoveColor = (parts[1] == "b") ? Color.black : Color.white;
            MoveNum = int.Parse(parts[5]);           
            MoveColor = Color.white;
        }

        private void InitFirures(string data)
        {
            for (int j = 8; j >= 2; j--)
            {
               data = data.Replace(j.ToString(), (j-1).ToString() + "1");               
            }
            data = data.Replace("1", ".");
            string[] lines = data.Split('/');
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    _figures[x, y] = lines[7-y][x] == '.' ? Figure.none : (Figure)lines[7 - y][x];
                }
            }
        }

        public  IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach (var square in  Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor() == MoveColor)
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square); 
                }
            }
        }

        private void GenerateFen()
        {
            this.Fen = FenFigures() + " " + (MoveColor == Color.white ? "w" : "b") + " - - 0 " + MoveNum.ToString();
        }

        private string FenFigures()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    stringBuilder.Append(_figures[x, y] == Figure.none ? '1' : (char)_figures[x, y]);
                }
                if (y > 0 )
                {
                    stringBuilder.Append('/');
                }
            }
            string eight = "11111111";
            for (int j = 8; j >=2; j--)
            {
                stringBuilder.Replace(eight.Substring(0, j), j.ToString());
            }
            return stringBuilder.ToString();
        }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
            {
                return _figures[square.X, square.Y];
            }
            return Figure.none;
        }

        public void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
            {
                _figures[square.X, square.Y] = figure;
            }
        }

        public Board Move(FigureMoving figureMoving)
        {
           Board nextBoard = new Board(Fen);
            nextBoard.SetFigureAt(figureMoving.squareFrom, Figure.none);
            nextBoard.SetFigureAt(figureMoving.squareTo, figureMoving.promotion == Figure.none ? figureMoving.figure : figureMoving.promotion);
            if (MoveColor == Color.black)
            {
                nextBoard.MoveNum++;
            }
            nextBoard.MoveColor = MoveColor.FlipColor();
            nextBoard.GenerateFen();
            return nextBoard;
        }        
    }
}
