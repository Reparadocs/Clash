using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashCore;

namespace ClashServer
{
    class ServerZones : Zones
    {
        public ServerZones() : base(new List<ZoneType> { ZoneType.Deck, ZoneType.Graveyard, ZoneType.Hand, ZoneType.Play }) { }

        public override void ShuffleDeck()
        {
            ZoneDict[ZoneType.Deck] = (List<Card>)ZoneDict[ZoneType.Deck].OrderBy(a => Guid.NewGuid());
        }

        public override Card ViewTopCardOfDeck()
        {
            if (ZoneDict[ZoneType.Deck].Count > 0)
            {
                return ZoneDict[ZoneType.Deck][0];
            }
            return null;
        }

        public override Card DrawFromDeck()
        {
            Card pop = ViewTopCardOfDeck();
            Transfer(ZoneType.Deck, ZoneType.Hand, pop);
            return pop;
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
