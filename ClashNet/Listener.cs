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
        TcpListener tcpListener;
        List<Client> connectedClients;
        IMessageObserver observer;
        int currentId; 

        public Listener(int port, IMessageObserver observer)
        {
            currentId = 0;
            this.observer = observer;
            connectedClients = new List<Client>();
            tcpListener = new TcpListener(IPAddress.Any, port);
            new Thread(new ThreadStart(tClientListener)).Start();
        }

        public void SendMessageToClient(byte[] message, int clientId)
        {
            new Thread(new ParameterizedThreadStart(tSendMessageToClient)).Start(new MessageWrapper(message, clientId));
        }

        private void tSendMessageToClient(object message)
        {
            MessageWrapper wrapper = (MessageWrapper)message;
            Client c = connectedClients.Find(i => i.Id == wrapper.ClientId);
            if (c != null && (c.State != ClientState.Disconnected))
            {
                c.SendMessage(wrapper.Message);
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
            Client c = new Client((TcpClient)client, observer, currentId);
            currentId++;
            connectedClients.Add(c);
            observer.OnNotify(new MessageWrapper(MessageType.Connect, null, c.Id));
        }
    }
}
