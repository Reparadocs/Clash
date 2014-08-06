using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Device : Card
    {
        public Card Attached { get; private set; }

        public Device(Player owner) : base(owner) { }

        public void OnAttach(Card card)
        {
            DeviceEffect();
            this.Attached = card;
        }

        public void OnDetach()
        {
            ReverseDeviceEffect();
            MoveTo(ZoneType.Graveyard);
        }

        public abstract void ReverseDeviceEffect();
        public abstract void DeviceEffect();
    }
}
