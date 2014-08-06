using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Creature : Card
    {
        public int Attack { get; set; }
        public int Health { get; protected set; }
        public int Armor { get; set; }
        public bool Sleeping { get; protected set; }

        public Creature(Player owner, int Attack, int Health)
            : base(owner)
        {
            Sleeping = true;
        }

        public void OnBeginTurn()
        {
            base.OnBeginTurn();
            Wake();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage - Armor;
        }

        public void TakeTrueDamage(int damage)
        {
            Health -= damage;
        }

        public void Wake()
        {
            Sleeping = false;
        }

    }
}
