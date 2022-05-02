using Microsoft.AspNetCore.SignalR;

namespace ChessGameWithFogOfWar.Hubs
{
    public class GameProcessHub : Hub
    {

        //public bool CheckIsRivalsКCompleted()
        //{
        //    Player PotentialTrailor = null;
        //    if (_queueProvider.CountWhite > 1 && _queueProvider.CountBlack == 0)
        //    {
        //        PotentialTrailor = _queueProvider.PeekedWhite;
        //    }
        //    if (_queueProvider.CountBlack > 1 && _queueProvider.CountWhite == 0)
        //    {
        //        PotentialTrailor = _queueProvider.PeekedBlack;
        //    }
        //    if (PotentialTrailor != null)
        //    {
        //        SendChangingColorProposal(PotentialTrailor); // todo
        //    }
        //    if (_queueProvider.CountWhite > 0 && _queueProvider.CountBlack > 0)
        //    {
        //        var CompletedRivals = _queueProvider.Dequeue();
        //        RedirectToGameProcessController(CompletedRivals); // todo
        //    }
        //}
    }
}
