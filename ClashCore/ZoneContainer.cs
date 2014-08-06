using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class ZoneContainer
    {
        public Dictionary<ZoneType, List<Card>> Zones { get; private set; }
        public List<Card> Cards { get; private set; }

        public ZoneContainer(List<Card> deck)
        {
            foreach (ZoneType zone in (ZoneType[]) Enum.GetValues(typeof(ZoneType)))
            {
                Zones.Add(zone, new List<Card>());
            }
            foreach(Card c in deck)
            {
                Add(ZoneType.Deck, c);
            }
        }

        public void Shuffle(ZoneType zone)
        {
            Zones[zone] = (List<Card>)Zones[zone].OrderBy(a => Guid.NewGuid());
        }

        public Card Peek(ZoneType zone)
        {
            if(Zones[zone].Count > 0)
            {
                return Zones[zone][0];
            }
            return null;
        }

        public void Pop(ZoneType zone, ZoneType moveTo)
        {
            if(Zones[zone].Count > 0)
            {
                Card popped = Zones[zone][0];
                Remove(zone, popped);
                Add(moveTo, popped);
            }
        }

        public void Add(ZoneType zone, Card card)
        {
            card.Zone = zone;
            card.OnEnter(zone);
            Cards.Add(card);
            Zones[zone].Add(card);
        }

        private void Remove(ZoneType zone, Card card)
        {
            card.OnExit(zone);
            Zones[zone].Remove(card);
        }

        public void Transfer(ZoneType newZone, Card card)
        {
            card.OnExit(card.Zone);
            Zones[card.Zone].Remove(card);
            Add(newZone, card);
        }

        public List<Card> SearchByCard<T>(ZoneType zone)
        {
            return (List<Card>)Zones[zone].OfType<T>();
        }
    }
}
