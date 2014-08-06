﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public abstract class Card
    {
        public ZoneType Zone { get; set; }

        public Player Owner { get; private set; }

        protected MatchObserver observer;

        public Card(Player owner)
        {
            this.Owner = owner;
            this.observer = owner.Match.Observer;
        }

        public void MoveTo(ZoneType zone)
        {
            Owner.ZoneContainer.Transfer(zone, this);
        }

        public void Draw(Player target, int numDraws)
        {
            for (int i = 0; i < numDraws; i++)
            {
                target.ZoneContainer.Pop(ZoneType.Deck, ZoneType.Hand);
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
            foreach(Creature c in Owner.ZoneContainer.Zones[ZoneType.Play])
            {
                c.TakeDamage(damage);
            }
            foreach(Creature c in Owner.Opponent.ZoneContainer.Zones[ZoneType.Play])
            {
                c.TakeDamage(damage);
            }
        }

        public void OnBeginTurn() { }
        public void OnEndTurn() { }
        public void OnOpponentBeginTurn() { }
        public void OnOpponentEndTurn() { }
        public void OnCardEnter(ZoneType zone, Card card) { }
        public void OnCardExit(ZoneType zone, Card card) { }

        public void OnEnter(ZoneType zone)
        {
            observer.OnNotify(NotificationType.EnterZone, this, zone, Owner);
        }
        public void OnExit(ZoneType zone)
        {
            observer.OnNotify(NotificationType.ExitZone, this, zone, Owner);
        }
    }
}
