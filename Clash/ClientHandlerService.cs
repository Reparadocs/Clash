using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using ClashNet;
using ClashCore;
using ClashCore.Serializables;

namespace Clash
{
    class ClientHandlerService : ISerializableObserver
    {
        ClientSerializer client;
        List<CardSerializer> cardMessageQueue;

        public ClientHandlerService()
        {
            cardMessageQueue = new List<CardSerializer>();
            TcpClient tcpClient = new TcpClient();
            client = new ClientSerializer(tcpClient, "127.0.0.1", 3000, this);
        }

        public void OnNotify(SerializableWrapper serializable)
        {
            if(serializable.Serializable is CardSerializer)
            {
                cardMessageQueue.Add((CardSerializer)serializable.Serializable);
            }
        }

        public CardSerializer GetCardSerializer(CardActions requestedAction)
        {
            CardSerializer card;
            do
            {
                card = cardMessageQueue.Last(c => c.Action == requestedAction);
            }
            while (card == null);
            cardMessageQueue.Remove(card);
            return card;
        }
        
        public void SendMessageToServer(ISerializable serializable)
        {
            client.SendSerializable(serializable);
        }
    }
}
