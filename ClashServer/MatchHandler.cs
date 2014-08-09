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
    class MatchHandler
    {
        public int[] PlayerIds { get; private set; }

        private Match match;

        public MatchHandler(int player1Id, int player2Id)
        {
            PlayerIds = new int[2] { player1Id, player2Id };
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

            players.Add(new Player("Mecha", deck, player1Id));
            players.Add(new Player("Corruption", deck, player2Id));

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
