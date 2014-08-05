using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ClashNet
{
    public class Listener
    {
        private class ClientIdMessageWrapper
        {
            public int clientId { get; private set; }
            public byte[] message { get; private set; }

            public ClientIdMessageWrapper(int clientId, byte[] message)
            {
                this.clientId = clientId;
                this.message = message;
            }
        }
        public ListenerState State { get; private set; }

        TcpListener tcpListener;
        List<Client> connectedClients;
        List<MessageWrapper> messages;
        int currentId; 

        public Listener(int port)
        {
            State = ListenerState.Active;
            currentId = 0;
            messages = new List<MessageWrapper>();
            connectedClients = new List<Client>();
            tcpListener = new TcpListener(IPAddress.Any, port);
            new Thread(new ThreadStart(tClientListener)).Start();
        }

        public List<MessageWrapper> GetMessages()
        {
            List<MessageWrapper> returnMessages = messages.ConvertAll(m => new MessageWrapper(m.ClientId, m.Type, m.Message));
            messages.Clear();
            State = ListenerState.Active;
            return returnMessages;
        }

        public void SendMessageToClient(int clientId, byte[] message)
        {
            new Thread(new ParameterizedThreadStart(tSendMessageToClient)).Start(new ClientIdMessageWrapper(clientId, message));
        }

        private void tSendMessageToClient(object clientIdMessage)
        {
            ClientIdMessageWrapper wrapper = (ClientIdMessageWrapper)clientIdMessage;
            Client c = connectedClients.Find(i => i.Id == wrapper.clientId);
            if (c != null && (c.State != ClientState.Disconnected))
            {
                c.SendMessage(wrapper.message);
            }
            else
            {
                throw new Exception("Client not connected");
            }
        }

        private void tClientListener()
        {
            tcpListener.Start();
            while(true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                new Thread(new ParameterizedThreadStart(tConnectClient)).Start(client);
            }
        }

        private void tConnectClient(object client)
        {
            Client c = new Client((TcpClient)client, currentId);
            currentId++;
            connectedClients.Add(c);
            messages.Add(new MessageWrapper(c.Id, MessageType.Connect, null));
            State = ListenerState.MessagesReceived;
            new Thread(new ParameterizedThreadStart(tClientMonitor)).Start(c);
        }

        private void tClientMonitor(object client)
        {
            Client monitored = (Client)client;
            while(true)
            {
                if(monitored.State != ClientState.Active)
                {
                    if(monitored.State != ClientState.MessagesReceived)
                    {
                        messages.Add(new MessageWrapper(monitored.Id, MessageType.Disconnect, null));
                    }
                    else
                    {
                        foreach(byte[] b in monitored.GetMessages())
                        {
                            messages.Add(new MessageWrapper(monitored.Id, MessageType.Normal, b));
                        }
                    }
                    State = ListenerState.MessagesReceived;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
