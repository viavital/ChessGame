namespace ChessGameWithFogOfWar.Model
{
    public class Rivals
    {
        
        public string GameId { get; set; }
        public Player WhitePlayer { get; set; }
        public Player BlackPlayer { get; set; }
        public Rivals()
        {
            Guid _guid = Guid.NewGuid();
            GameId = _guid.ToString();   
        }
    }
}
