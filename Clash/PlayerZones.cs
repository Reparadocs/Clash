using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClashCore;

namespace Clash
{
    class PlayerZones : Zones
    {
        private ClientHandlerService client;
        private CardNameService cardNameService;

        public PlayerZones() 
            : base(new List<ZoneType> {ZoneType.Hand, ZoneType.Play, ZoneType.Graveyard})
        {
            cardNameService = ServiceLocator.GetService<CardNameService>();
            client = GameServiceProvider.GetService<ClientHandlerService>();
        }

        public override void ShuffleDeck() { } //Actual shuffling performed on server

        public override Card ViewTopCardOfDeck(int clientId)
        {
            return cardNameService.GetNewCardInstance(client.GetCardSerializer(CardActions.View).TargetCard); 
        }

    }
}
