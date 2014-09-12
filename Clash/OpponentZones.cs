using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClashCore;
using ClashCore.Cards;

namespace Clash
{
    class OpponentZones : Zones
    {
        private Card mockCard;

        public OpponentZones() : base(new List<ZoneType> { ZoneType.Play, ZoneType.Graveyard }) 
        {
            mockCard = new Fastlane(null);
        }

        public override void ShuffleDeck() { }

        public override Card ViewTopCardOfDeck(int clientId) { return mockCard; }

        public override Card DrawFromDeck(int clientId) { return mockCard; }

        public override void PlayFromHand(Card card, int clientId)
        {
            throw new NotImplementedException();
        }

        public void AddCardToPlay(Card card)
        {
            Add(ZoneType.Play, card);
        }

    }
}
