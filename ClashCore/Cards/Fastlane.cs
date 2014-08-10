using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore.Cards
{
    public class Fastlane : Environment
    {
        public override int Cost 
        { 
            get { return 2; } 
        }

        public override String Name
        {
            get { return "Fastlane"; }
        }
        public Fastlane(Player owner) : base(owner) { }

        public override void OnCardEnter(ZoneType zone, Card card, Player player)
        {
            base.OnCardEnter(zone, card, player);
            if(zone == ZoneType.Play && card is Creature)
            {
                ((Creature)card).Wake();
            }
        }
    }
}
