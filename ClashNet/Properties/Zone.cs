using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Zone
    {
        public List<Card> Cards { get; private set; }

        public ZoneContainer(List<Card> cards)
        {
            this.Cards = cards;
        }
    }
}
