using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashNet;
using ClashCore;
using ClashCore.Cards;

namespace ClashServer
{
    class MatchHandler : ISerializableObserver
    {
        public int[] PlayerIds { get; private set; }

        private Match match;
        
        public MatchHandler(int[] playerIds)
        {
            if(playerIds.Length > Global.NumPlayers)
            {
                throw new Exception("Too many/few players");
            }
            this.PlayerIds = playerIds;
            List<Player> players = new List<Player>();
            List<Card> deck = new List<Card>();
            List<Card> deck2 = new List<Card>();

            deck.Add(new Fastlane(players[0]));
            deck.Add(new GridSurge(players[0]));
            deck.Add(new HeatVision(players[0]));
            deck.Add(new Helmet(players[0]));
            deck.Add(new Voidling(players[0]));

            deck2.Add(new Fastlane(players[0]));
            deck2.Add(new GridSurge(players[0]));
            deck2.Add(new HeatVision(players[0]));
            deck2.Add(new Helmet(players[0]));
            deck2.Add(new Voidling(players[0]));

            ServerZones p1Zone = new ServerZones();
            ServerZones p2Zone = new ServerZones();
            p1Zone.InitializeDeck(deck);
            p2Zone.InitializeDeck(deck2);

            players.Add(new Player("Mecha", p1Zone, playerIds[0]));
            players.Add(new Player("Corruption", p2Zone, playerIds[1]));

            match = new Match(players);
        }
        public void OnNotify(SerializableWrapper serializable)
        {
            if(!PlayerIds.Contains(serializable.ClientId))
            {
                throw new Exception("This message is not from a client in this game");
            }
        }
    }
}
