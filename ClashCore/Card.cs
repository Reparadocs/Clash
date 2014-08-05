using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Card
    {
        public abstract void OnEnter(ZoneType zone);
        public abstract void OnExit(ZoneType zone);

    }
}
