namespace ChessCore
{
    public class Chess
    {
        public string GameId;
        public string WhitePlayerId;
        public string BlackPlayerId;
        public string Fen { get; private set; }
        Board Board;
        Moves Moves;
        List<FigureMoving> AllMoves;
        public Chess (string GameId,string WhitePlayerId, string BlackPlayerId, string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") 
        {
            this.WhitePlayerId = WhitePlayerId;
            this.BlackPlayerId = BlackPlayerId;
            this.GameId = GameId;
            Fen = fen;
            Board = new Board (fen);
            Moves = new Moves(Board);
            FindAllMoves ();
        }
        Chess(Board board, string GameId, string WhitePlayerId, string BlackPlayerId)
        {
            this.GameId=GameId;
            this.WhitePlayerId = WhitePlayerId;
            this.BlackPlayerId = BlackPlayerId;
            Board = board;
            this.Fen = board.Fen;
            Moves = new Moves(Board);
        }
        public Chess Move (string move)
        {
            FigureMoving figureMoving = new FigureMoving(move);
            if (!Moves.CanMove(figureMoving))
            {
                return this;
            }
            Board nextBoard = Board.Move(figureMoving);
            Chess nextChess = new Chess (nextBoard, this.GameId, this.WhitePlayerId, this.BlackPlayerId);            
            return nextChess;
        }  
        public char GetFigureAt(int x, int y)
        {
            Square square = new Square (x, y);
            Figure figure = Board.GetFigureAt(square);
            return figure == Figure.none ? '.' : (char)figure;
        }
        public Color ReturnMoveColor()
        {
            return Board.MoveColor;
        }
        void FindAllMoves()
        {
            AllMoves = new List<FigureMoving>();
            foreach (FigureOnSquare fs in Board.YieldFigures())
            {
                foreach (Square squareTo in Square.YieldSquares())
                {
                    FigureMoving figureMoving = new FigureMoving (fs, squareTo);
                    if (Moves.CanMove(figureMoving))
                    {
                        AllMoves.Add(figureMoving);
                    }
                }
            }
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();
            foreach (var figureMoving in AllMoves)
            {
                list.Add(figureMoving.ToString());
            }
            return list;
        }
    }
}