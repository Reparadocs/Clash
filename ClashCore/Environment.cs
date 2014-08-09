using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Environment : Card
    {
        public Environment(Player owner) : base(owner) { }

        public override void OnCardEnter(ZoneType zone, Card card, Player player)
        {
            base.OnCardEnter(zone, card, player);
            if(zone == ZoneType.Play && card is Environment)
            {
                Die();
            }
        }
    }
}
