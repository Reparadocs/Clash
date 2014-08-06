using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Spell : Card
    {
        public Spell(Player owner) : base(owner) { }
        public override void OnEnter(ZoneType zone)
        {
            base.OnEnter(zone);
            if(zone == ZoneType.Play)
            {
                SpellEffect();
            }
        }

        public abstract void SpellEffect();
    }
}
