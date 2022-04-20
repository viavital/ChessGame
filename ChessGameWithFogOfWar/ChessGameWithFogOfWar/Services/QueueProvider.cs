using ChessGameWithFogOfWar.Model;

namespace ChessGameWithFogOfWar.Services
{
    public class QueueProvider
    {
        private Queue<Player> WhitePlayersQueue = new() ;

        private Queue<Player> BlackPlayersQueue = new() ;

        public void Enqueue(Player player, ColorOfTeamEnum colorOfTeam)
        {
            if (colorOfTeam == ColorOfTeamEnum.White)
            {
                WhitePlayersQueue.Enqueue(player);
            }
            else
            {
                BlackPlayersQueue.Enqueue(player);
            }
        }
        public Player[] Dequeue()
        {
            var WhitePlayer = WhitePlayersQueue.Dequeue();
            var BlackPlayer = BlackPlayersQueue.Dequeue();

            return new Player[] {WhitePlayer, BlackPlayer};
        }
        public bool Contains(Player player)
        {
            return WhitePlayersQueue.Contains(player) || BlackPlayersQueue.Contains(player);
        }

        public int CountWhite => WhitePlayersQueue.Count;
        public int CountBlack => BlackPlayersQueue.Count;

        public Player PeekedWhite => WhitePlayersQueue.Peek();
        public Player PeekedBlack => BlackPlayersQueue.Peek();
    }
}
