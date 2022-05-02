using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore
{
    internal class FigureMoving
    {
        public Figure figure { get; set; }
        public Square squareFrom { get; set; }
        public Square squareTo { get; set; }
        public Figure promotion { get; set; } // if Pawn is to become Queen
        public FigureMoving(FigureOnSquare figureOnSquare, Square to, Figure promotion = Figure.none)
        {
            figure = figureOnSquare.figure;
            squareFrom = figureOnSquare.square;
            squareTo = to;
            this.promotion = promotion;
        }

        public FigureMoving(string move) // Pe2e4  Pe7e8Q 
                                         // 01234  012345  
        {
            figure = (Figure)move[0];
            squareFrom = new Square(move.Substring(1,2)); 
            squareTo = new Square(move.Substring(3,2));
            promotion = (move.Length == 6) ? (Figure)move[5]: Figure.none;
        }
        public int DeltaX { get { return squareTo.X - squareFrom.X; } }
        public int DeltaY { get { return squareTo.Y - squareFrom.Y; } }
        public int AbsDeltaX { get { return Math.Abs(DeltaX); } }
        public int AbsDeltaY { get { return Math.Abs(DeltaY); } }
        public int SignDeltaX { get { return Math.Sign(DeltaX); } }
        public int SignDeltaY { get { return Math.Sign(DeltaY); } }

        public override string ToString()
        {
            string text = (char)figure + squareFrom.Name + squareTo.Name;
            if (promotion != Figure.none)
            {
                text += (char)promotion;
            } 
            return text;
        }
    }
}
