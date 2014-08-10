using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore.Cards
{
    public class HeatVision : Spell
    {
        public override int Cost
        {
            get { return 3; }
        }

        public override string Name
        {
            get { return "Heat Vision"; }
        }
        private static int numDraws = 2;
        public HeatVision(Player owner) : base(owner) { }

        public override void SpellEffect()
        {
            Draw(Owner, numDraws);
        }
    }
}
