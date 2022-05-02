using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCore
{
    internal class Moves
    {
        FigureMoving _figureMoving;
        Board _board;
        public Moves(Board board)
        {
            _board = board;
        }

        public bool CanMove(FigureMoving figureMoving)
        {
            _figureMoving = figureMoving;
            return CanMoveFrom() && CanMoveTo() && CanFigureMove();
        }

        private bool CanFigureMove()
        {
            switch (_figureMoving.figure)
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();

                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();

                case Figure.whiteRook:
                case Figure.blackRook:
                    return (_figureMoving.SignDeltaX == 0 || _figureMoving.SignDeltaY == 0) &&
                        CanStraightMove();

                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return (_figureMoving.SignDeltaX != 0 || _figureMoving.SignDeltaY != 0) &&
                        CanStraightMove();

                case Figure.whiteKnight:
                case Figure.blackKnight:
                   return CanKnightMove();
                   
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();
                default:
                    return false;

            }
        }

        private bool CanPawnMove()
        {
            if ( _figureMoving.squareFrom.Y < 0 || _figureMoving.squareFrom.Y > 6)
            {
                return false;
            }
            int stepY = _figureMoving.figure.GetColor() == Color.white ? 1 : -1;
            return CanPawnGo(stepY) || CanPawnJump(stepY) || CanPawnEat(stepY);
        }

        private bool CanPawnEat(int stepY)
        {
            if (_board.GetFigureAt(_figureMoving.squareTo) != Figure.none)
                if (_figureMoving.AbsDeltaX == 1)
                    if (_figureMoving.DeltaY == stepY)
                        return true;
            return false;
            
        }

        private bool CanPawnJump(int stepY)
        {
            if (_board.GetFigureAt(_figureMoving.squareTo) == Figure.none)
                if (_figureMoving.DeltaX == 0)
                    if (_figureMoving.DeltaY == 2 * stepY)
                        if (_figureMoving.squareFrom.Y == 1 || _figureMoving.squareFrom.Y == 6)
                            if (_board.GetFigureAt(new Square(_figureMoving.squareFrom.X, _figureMoving.squareFrom.Y + stepY)) == Figure.none)
                                return true;
            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if (_board.GetFigureAt(_figureMoving.squareTo) == Figure.none)
                if (_figureMoving.DeltaX == 0)
                    if (_figureMoving.DeltaY == stepY)
                        return true;
            return false;
               
        }

        private bool CanStraightMove()
        {
            Square squareAt = _figureMoving.squareFrom;
            do
            {
                squareAt = new Square(squareAt.X + _figureMoving.SignDeltaX, squareAt.Y + _figureMoving.SignDeltaY);
                if (squareAt == _figureMoving.squareTo)
                {
                    return true;
                }
            } while (squareAt.OnBoard() && _board.GetFigureAt(squareAt) == Figure.none);
            return false;
        }
        
        private bool CanKingMove()
        {
            if (_figureMoving.AbsDeltaX <= 1 && _figureMoving.AbsDeltaY <= 1)
            {
                return true;
            }
            return false;
        }

        private bool CanKnightMove()
        {
            if (_figureMoving.AbsDeltaX == 1 && _figureMoving.AbsDeltaY == 2)
                return true;
            if (_figureMoving.AbsDeltaX == 2 && _figureMoving.AbsDeltaY == 1)
                return true;
            return false;
        }

        private bool CanMoveTo()
        {
            return _figureMoving.squareTo.OnBoard() &&
                _figureMoving.squareFrom != _figureMoving.squareTo && 
                _board.GetFigureAt(_figureMoving.squareTo).GetColor() != _board.MoveColor;
        }

        private bool CanMoveFrom()
        {
            return _figureMoving.squareFrom.OnBoard() && _figureMoving.figure.GetColor() == _board.MoveColor;
        }
    }
}
