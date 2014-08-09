using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore.Cards
{
    public class GridSurge : Spell
    {
        public override int Cost
        {
            get { return 3; }
        }
        private static int spellDamage = 1;

        public GridSurge(Player owner) : base(owner) { }
        public override void SpellEffect()
        {
            DamageAll(spellDamage);
            DamageBothPlayers(spellDamage);
        }
    }
}
