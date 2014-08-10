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

        public abstract Card ViewTopCardOfDeck();

        public abstract Card DrawFromDeck();

        public abstract void InitializeDeck(List<Card> deck);

        public void PlayFromHand(Card card)
        {
            Transfer(ZoneType.Hand, ZoneType.Play, card);
        }

        public void GraveyardFromPlay(Card card)
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
            card.Zone = newZone;
            ZoneDict[newZone].Add(card);
            card.OnEnter(newZone);
        }

        /*
        public List<Card> SearchByCard<T>(ZoneType zone)
        {
            return (List<Card>)Zones[zone].OfType<T>();
        }
        */
    }
}
