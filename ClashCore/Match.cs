using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class Match
    {
        public List<Player> Players { get; private set; }

        public MatchObserver Observer { get; private set; }

        public Match(List<Player> players)
        {
            if(players.Count != Global.NumPlayers)
            {
                throw new Exception("Unexpected number of players");
            }
            this.Players = players;
            for(int i = 0; i < Global.NumPlayers; i++)
            {
                Players[i].Opponent = Players[(i * -1) + 1];
                Players[i].Match = this;
            }
            Observer = new MatchObserver(this);
            Players[0].BeginTurn();
        }
    }
}
