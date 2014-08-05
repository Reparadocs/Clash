using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class Match
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        int curTurn = 0;

        public Match(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
        }

    }
}
