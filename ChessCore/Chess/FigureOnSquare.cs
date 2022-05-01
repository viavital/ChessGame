using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore
{
    internal class FigureOnSquare
    {
        public Figure figure { get; set; }
        public Square square { get; set; }
        public FigureOnSquare(Figure figure, Square square)
        {
            this.figure = figure;
            this.square = square;   
        }
    }
}
