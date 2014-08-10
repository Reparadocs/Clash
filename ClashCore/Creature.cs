using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Creature : Card
    {
        public int Attack { get; private set; }
        public int Health { get; private set; }
        public int Armor { get; private set; }
        public bool Sleeping { get; private set; }

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

        public virtual void ChangeAttack(int attackDifference)
        {
            Attack += attackDifference;
        }

        public virtual void SetAttack(int newAttack)
        {
            Attack = newAttack;
        }

        public virtual void ChangeArmor(int armorDifference)
        {
            Armor += armorDifference;
        }

        public virtual void SetArmor(int newArmor)
        {
            Armor = newArmor;
        }

        public virtual void Wake()
        {
            Sleeping = false;
        }

    }
}
