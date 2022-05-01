using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore
{
    struct Square
    {
       public static Square none = new Square(-1, -1); 
       public int X { get;private set; }
       public int Y { get; private set; }
        public string Name { get { return ((char)('a' + X)).ToString() + (Y + 1).ToString(); } }

        public Square(int x, int y)
        {
            this.X = x;
            this.Y = y; 
        }
        public Square(string e2)
        {
            if (e2[0] >= 'a' && e2[0] <= 'h' && e2[1] >= '1' && e2[1] <= '8')
            {
                X = e2[0] - 'a';
                Y = e2[1] - '1';
            }
            else this = none;
        }

        public bool OnBoard()
        {
            return X >= 0 && X < 8 &&
                   Y >= 0 && Y < 8;
        }

        public static bool operator ==(Square square1, Square square2) => square1.X == square2.X && square1.Y == square2.Y;
        public static bool operator !=(Square square1, Square square2) => square1.X != square2.X || square1.Y != square2.Y;

        internal static IEnumerable<Square> YieldSquares()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    yield return new Square(x, y);
                }
            }
        }
    }
}
