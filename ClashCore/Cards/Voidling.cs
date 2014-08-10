using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore.Cards
{
    public class Voidling : Creature
    {
        public override int Cost
        {
            get { return 1; }
        }
        private static int initialAttack = 1;
        private static int initialHealth = 1;
        private static int attackIncrease = 1;

        public Voidling(Player owner) : base(owner, initialAttack, initialHealth) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            ChangeAttack(attackIncrease);
        }

    }
}
