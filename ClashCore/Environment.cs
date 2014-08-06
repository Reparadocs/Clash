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
    }
}
