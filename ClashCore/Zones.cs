using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Zones
    {
        protected Dictionary<ZoneType, List<Card>> ZoneDict { get; private set; }

        public Zones(List<ZoneType> accessibleZones)
        {
            foreach (ZoneType zone in accessibleZones)
            {
                ZoneDict.Add(zone, new List<Card>());
            }
        }

        public abstract void ShuffleDeck();

        public abstract Card ViewTopCardOfDeck(int clientId);

        public abstract Card DrawFromDeck(int clientId);

        public virtual void PlayFromHand(Card card, int clientId)
        {
            Transfer(ZoneType.Hand, ZoneType.Play, card);
        }

        public virtual void GraveyardFromPlay(Card card)
        {
            Transfer(ZoneType.Play, ZoneType.Graveyard, card);
        }

        public List<Card> GetCardsInPlay()
        {
            return ZoneDict[ZoneType.Play];
        }

        public List<Card> GetCardsInGraveyard()
        {
            return ZoneDict[ZoneType.Deck];
        }

        protected void Transfer(ZoneType currentZone, ZoneType newZone, Card card)
        {
            ZoneDict[currentZone].Remove(card);
            card.OnExit(currentZone);
            Add(newZone, card);
        }

        protected void Add(ZoneType zone, Card card)
        {
            card.Zone = zone;
            ZoneDict[zone].Add(card);
            card.OnEnter(zone);
        }

        public Card SearchForCard(String cardName, ZoneType zone)
        {
            return ZoneDict[zone].First(c => c.Name == cardName);
        }
    }
}
