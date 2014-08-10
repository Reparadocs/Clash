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
        public Zones Zones { get; private set; }

        public Player(string name, Zones zones)
        {
            this.Zones = zones;
            this.Name = name;
            this.Energy = Global.StartingEnergy;
            this.Health = Global.PlayerHealth;
        }

        public Player(string name, Zones zones, int clientId) 
            : this(name, zones)
        {
            this.ClientId = clientId;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void Heal(int heal)
        {
            Health += heal;
        }

        public void AddEnergy(int addedEnergy)
        {
            Energy += addedEnergy;
        }

        public void RaiseEnergyCap(int addedCap)
        {
            EnergyCap += addedCap;
        }

        public void BeginGame()
        {
            for(int i = 0; i < Global.NumberOfStartingCards; i++)
            {
                Zones.DrawFromDeck();
            }
        }

        public void BeginTurn()
        {
            Match.Observer.OnNotify(NotificationType.BeginTurn, null, ZoneType.Deck, this);
            AddEnergy(1);
            RaiseEnergyCap(1);
            Zones.DrawFromDeck();
        }

        public void EndTurn()
        {
            Match.Observer.OnNotify(NotificationType.EndTurn, null, ZoneType.Deck, this);
        }
    }
}
