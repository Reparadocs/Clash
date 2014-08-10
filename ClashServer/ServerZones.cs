using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashCore;
using ClashNet;
using ClashCore.Serializables;

namespace ClashServer
{
    class ServerZones : Zones
    {
        private ListenerSerializer listener;
        private int[] playerIds;

        public ServerZones(ListenerSerializer listener, int[] playerIds) 
            : base(new List<ZoneType> { ZoneType.Deck, ZoneType.Graveyard, ZoneType.Hand, ZoneType.Play }) 
        {
            this.playerIds = playerIds;
            this.listener = listener;
        }

        public override void ShuffleDeck()
        {
            ZoneDict[ZoneType.Deck] = (List<Card>)ZoneDict[ZoneType.Deck].OrderBy(a => Guid.NewGuid());
        }

        public override Card ViewTopCardOfDeck(int clientId)
        {
            if (ZoneDict[ZoneType.Deck].Count > 0)
            {
                Card peek = ZoneDict[ZoneType.Deck][0];
                listener.SendSerializableToClient(new CardSerializer(peek.GetType(), CardActions.View), clientId);
                return peek;
            }
            listener.SendSerializableToClient(new CardSerializer(null, CardActions.View), clientId);
            return null;
        }

        public override Card DrawFromDeck(int clientId)
        {
            if (ZoneDict[ZoneType.Deck].Count > 0)
            {
                Card pop = ZoneDict[ZoneType.Deck][0];
                listener.SendSerializableToClient(new CardSerializer(pop.GetType(), CardActions.Draw), clientId);
                Transfer(ZoneType.Deck, ZoneType.Hand, pop);
                return pop;
            }
            listener.SendSerializableToClient(new CardSerializer(null, CardActions.Draw), clientId);
            return null;
        }

        public override void PlayFromHand(Card card, int clientId)
        {
            base.PlayFromHand(card, clientId);
            listener.SendSerializableToClient(new CardSerializer(card.GetType(), CardActions.Play), GetOtherClientId(clientId));
        }

        public override void InitializeDeck(List<Card> deck)
        {
            CheckDeck(deck);
            foreach(Card c in deck)
            {
                ZoneDict[ZoneType.Deck].Add(c);
                c.Zone = ZoneType.Deck;
                c.OnEnter(ZoneType.Deck);
            }
        }

        private int GetOtherClientId(int clientId)
        {
            return (clientId != playerIds[0]) ? playerIds[1] : playerIds[0];
        }

        private void CheckDeck(List<Card> deck)
        {
            if (deck.Count < Global.DeckSize)
            {
                throw new Exception("Too few cards in deck");
            }
            Dictionary<Type, int> instances = new Dictionary<Type, int>();
            foreach (Card c in deck)
            {
                if (instances.ContainsKey(c.GetType()))
                {
                    instances.Add(c.GetType(), 1);
                }
                else
                {
                    instances[c.GetType()] += 1;
                    if (Global.MaxDuplicates < instances[c.GetType()])
                    {
                        throw new Exception("Too many instances of a card");
                    }
                }
            }
        }
    }
}
