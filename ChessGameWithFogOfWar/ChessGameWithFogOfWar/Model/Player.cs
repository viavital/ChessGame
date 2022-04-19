namespace ChessGameWithFogOfWar.Model
{
    public class ReceivedPostData
    {
        public Player Player { get; set; }
        public PlayersColor PlayersColor { get; set; }
    }
    public class Player
    {
        public Player()
        {
            Guid _guid = Guid.NewGuid();
            Id = _guid;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }        
    }
    public class PlayersColor
    {
        public string Color { get; set; }

        public ColorOfTeamEnum ReturnColorEnum()
        {
            if (Color == "White" || Color == "white" )
            {
                return ColorOfTeamEnum.White;
            }
            return ColorOfTeamEnum.Black;
        }

    }
}
