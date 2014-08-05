using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class Player
    {
        public int Health { get; private set; }
        public int ClientId { get; private set; }
        public string Name { get; private set; }

        public Player(string name)
        {
            this.Name = name;
            this.Health = Global.PlayerHealth;
        }

        public Player(string name, int clientId) 
            : this(name)
        {
            this.ClientId = clientId;
        }
    }
}
