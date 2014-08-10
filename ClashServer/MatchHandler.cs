using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashNet;
using ClashCore;
using ClashCore.Cards;
using ClashCore.Serializables;

namespace ClashServer
{
    class MatchHandler : ISerializableObserver
    {
        public int[] PlayerIds { get; private set; }

        private ListenerSerializer listener;
        private Match match;
        private Dictionary<int, Player> idPlayerDict;
        
        public MatchHandler(int[] playerIds, ListenerSerializer listener)
        {
            this.listener = listener;
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

            ServerZones p1Zone = new ServerZones(listener, PlayerIds);
            ServerZones p2Zone = new ServerZones(listener, PlayerIds);
            p1Zone.InitializeDeck(deck);
            p2Zone.InitializeDeck(deck2);

            idPlayerDict = new Dictionary<int, Player>();
            players.Add(new Player("Mecha", p1Zone, playerIds[0]));
            players.Add(new Player("Corruption", p2Zone, playerIds[1]));

            idPlayerDict.Add(players[0].ClientId, players[0]);
            idPlayerDict.Add(players[1].ClientId, players[1]);

            match = new Match(players);
        }
        public void OnNotify(SerializableWrapper serializable)
        {
            if(serializable.Serializable is CardSerializer)
            {
                CardSerializer card = (CardSerializer)serializable.Serializable;
                if(card.Action == CardActions.Play)
                {
                    idPlayerDict[serializable.ClientId].Zones.SearchForCard(card.TargetCard, ZoneType.Hand).Play();
                }
            }
        }
    }
}
