using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore.Cards
{
    public class Helmet : Device
    {
        public override int Cost
        {
            get { return 2; }
        }
        private static int armorValue = 1;

        public Helmet(Player owner) : base(owner) { }

        public override void DeviceEffect()
        {
            if (Attached is Creature)
            {
                ((Creature)Attached).Armor += armorValue;
            }
        }

        public override void ReverseDeviceEffect()
        {
            if(Attached is Creature)
            {
                ((Creature)Attached).Armor -= armorValue;
            }
        }
    }
}
