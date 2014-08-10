using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Card
    {
        public abstract int Cost { get; }
        public ZoneType Zone { get; set; }
        public Player Owner { get; private set; }

        protected MatchObserver observer;

        public Card(Player owner)
        {
            this.Owner = owner;
            this.observer = owner.Match.Observer;
        }

        public void Play()
        {
            if(Owner.Energy >= Cost)
            {
                Owner.Zones.PlayFromHand(this);
            }
            else
            {
                throw new Exception("Not enough energy to play card");
            }
        }

        public void Die()
        {
            Owner.Zones.GraveyardFromPlay(this);
        }

        public void Draw(Player target, int numDraws)
        {
            for (int i = 0; i < numDraws; i++)
            {
                target.Zones.DrawFromDeck();
            }
        }

        public void DamageOpponent(int damage)
        {
            Owner.Opponent.TakeDamage(damage);
        }

        public void DamageOwner(int damage)
        {
            Owner.TakeDamage(damage);
        }

        public void DamageBothPlayers(int damage)
        {
            DamageOpponent(damage);
            DamageOwner(damage);
        }

        public void DamageAll(int damage)
        {
            foreach(Creature c in Owner.Zones.GetCardsInPlay())
            {
                c.TakeDamage(damage);
            }
            foreach(Creature c in Owner.Opponent.Zones.GetCardsInPlay())
            {
                c.TakeDamage(damage);
            }
        }

        protected bool IsOwner(Player player)
        {
            if(player.Equals(Owner))
            {
                return true;
            }
            return false;
        }

        public virtual void OnBeginTurn(Player player) { }
        public virtual void OnEndTurn(Player player) { }
        public virtual void OnCardEnter(ZoneType zone, Card card, Player player) { }
        public virtual void OnCardExit(ZoneType zone, Card card, Player player) { }

        public virtual void OnEnter(ZoneType zone)
        {
            observer.OnNotify(NotificationType.EnterZone, this, zone, Owner);
        }
        public virtual void OnExit(ZoneType zone)
        {
            observer.OnNotify(NotificationType.ExitZone, this, zone, Owner);
        }
    }
}
