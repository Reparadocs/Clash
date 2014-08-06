using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore.Cards
{
    public class GridSurge : Spell
    {
        private static int spellDamage = 1;

        public GridSurge(Player owner) : base(owner) { }
        public override void SpellEffect()
        {
            DamageAll(spellDamage);
            DamageBothPlayers(spellDamage);
        }
    }
}
