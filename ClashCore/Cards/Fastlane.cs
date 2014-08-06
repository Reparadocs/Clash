using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore.Cards
{
    public class Fastlane : Environment
    {
        public Fastlane(Player owner) : base(owner) { }

        public void OnCardEnter(ZoneType zone, Card card)
        {
            base.OnCardEnter(zone, card);
            if(zone == ZoneType.Play && card is Creature)
            {
                ((Creature)card).Wake();
            }
        }
    }
}
