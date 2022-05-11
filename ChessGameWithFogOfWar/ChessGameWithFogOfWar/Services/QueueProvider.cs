using ChessCore;
using ChessGameWithFogOfWar.Model;

namespace ChessGameWithFogOfWar.Services
{
    public class QueueProvider
    {
        private Queue<Player> WhitePlayersQueue = new() ;

        private Queue<Player> BlackPlayersQueue = new() ;

        Rivals _rivals;

        static public List<Chess> _chess = new List<Chess>();

        public event Action<Rivals> RivalsCompletedEvent;

        private readonly object _queueLockObj = new object();

        public Player Enqueue(Player player, ColorOfTeamEnum colorOfTeam)
        {
            lock (_queueLockObj)
            {
                if (colorOfTeam == ColorOfTeamEnum.White)
                {
                    WhitePlayersQueue.Enqueue(player);
                    return WhitePlayersQueue.Peek();
                }
                    BlackPlayersQueue.Enqueue(player);
                    return BlackPlayersQueue.Peek();
            }
        }
        public Rivals Dequeue()
        {
            lock (_queueLockObj)
            {
                Rivals rivals = new Rivals();
                rivals.WhitePlayer = WhitePlayersQueue.Dequeue();
                rivals.BlackPlayer = BlackPlayersQueue.Dequeue();
                return rivals;
            }
        }
        public Player ReturnPlayer(string PlayersId)
        {
            lock (_queueLockObj)
            {
                Player player = WhitePlayersQueue.FirstOrDefault(p => p.Id.ToString() == PlayersId);
                if (player == null)
                {
                    player = BlackPlayersQueue.FirstOrDefault(p => p.Id.ToString() == PlayersId);
                }
                return player;
            }
        }

        public bool Contains(Player player)
        {
            lock (_queueLockObj)
            {
                return WhitePlayersQueue.Contains(player) || BlackPlayersQueue.Contains(player);
            }
        }

        public string DeletePlayerFromQueue (string Id)
        {            
            var user = WhitePlayersQueue.FirstOrDefault(u => u.Id.ToString() == Id);
            if (user != null)
            {
                WhitePlayersQueue = new Queue<Player>(WhitePlayersQueue.Where(u => u.Id.ToString() != Id));
            }
            else
            {
                user = BlackPlayersQueue.FirstOrDefault(u => u.Id.ToString() == Id);
                BlackPlayersQueue = new Queue<Player>(BlackPlayersQueue.Where(u => u.Id.ToString() != Id));
            }            
            if (user == null) 
               return "User was removed from queue before request";

            return $"User {user.Name} removed from queue";
        }
        public async Task<bool> CheckIsRivalsКCompleted()
        {
                if (CountWhite > 0 && CountBlack > 0)
                {
                    _rivals = Dequeue();
                    RivalsCompletedEvent(_rivals);
                    return true;
                }
                return false;
        }
        public int CountWhite => WhitePlayersQueue.Count;
        public int CountBlack => BlackPlayersQueue.Count;

        public Player PeekedWhite => WhitePlayersQueue.Peek();
        public Player PeekedBlack => BlackPlayersQueue.Peek();
    }
}
