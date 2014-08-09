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
        public int EnergyCap { get; private set; }
        public int Energy { get; private set; }
        public int ClientId { get; private set; }
        public string Name { get; private set; }
        public Match Match { get; set; }
        public Player Opponent { get; set; }
         
        public ZoneContainer ZoneContainer { get; private set; }

        public Player(string name, List<Card> deck)
        {
            ZoneContainer = new ZoneContainer(deck);
            this.Name = name;
            this.Energy = Global.StartingEnergy;
            this.Health = Global.PlayerHealth;
        }

        public Player(string name, List<Card> deck, int clientId) 
            : this(name, deck)
        {
            this.ClientId = clientId;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void BeginTurn()
        {
            Match.Observer.OnNotify(NotificationType.BeginTurn, null, ZoneType.Deck, this);
            EnergyCap += 1;
            Energy += 1;
            ZoneContainer.Pop(ZoneType.Deck, ZoneType.Hand);
        }

        public void EndTurn()
        {
            Match.Observer.OnNotify(NotificationType.EndTurn, null, ZoneType.Deck, this);
        }
    }
}
