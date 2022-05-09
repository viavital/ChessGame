using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConnectionSignalR
{
    internal class UpdateIdConnectionMessage
    {
        public string PlayersId { get; set; }
        public string ConnectionId { get; set; }
    }
}
