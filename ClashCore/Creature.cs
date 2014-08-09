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

        public override void OnBeginTurn(Player player)
        {
            base.OnBeginTurn(player);
            if(IsOwner(player))
            {
                Wake();
            }
        }

        public virtual void TakeDamage(int damage)
        {
            TakeTrueDamage(damage - Armor);
        }

        public virtual void TakeTrueDamage(int damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                Die();
            }
        }

        public virtual void Wake()
        {
            Sleeping = false;
        }

    }
}
