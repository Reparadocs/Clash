using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class Zone
    {
        public ZoneType Type { get; private set; }
        public List<Card> Cards { get; protected set; }

        public Zone(ZoneType type, List<Card> cards)
        {
            this.Type = type;
            this.Cards = cards;
        }

        public void Shuffle()
        {
            Cards = (List<Card>)Cards.OrderBy(a => Guid.NewGuid());
        }

        public Card Peek()
        {
            if(Cards.Count > 0)
            {
                return Cards[0];
            }
            return null;
        }

        public Card Pop()
        {
            if(Cards.Count > 0)
            {
                Cards.RemoveAt(0);
                return Cards[0];
            }
            return null;
        }

        public List<Card> SearchByCard<T>()
        {
            return (List<Card>)Cards.OfType<T>();
        }
    }
}
