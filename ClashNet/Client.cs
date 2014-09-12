using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ClashNet
{
    public class Client
    {
        public ClientState State { get; private set; }
        public int Id { get; private set; }

        TcpClient tcpClient;
        NetworkStream stream;
        IMessageObserver observer;
        
        public Client(TcpClient client, IMessageObserver observer, int id)
        {
            this.Id = id;
            this.observer = observer;
            tcpClient = client;
            stream = client.GetStream();
            State = ClientState.Active;
            new Thread(new ThreadStart(tReceive)).Start();
        }

        public Client(TcpClient client, string ipAddress, int port, IMessageObserver observer)
        {
            tcpClient = client;
            client.Connect(ipAddress, port);
            stream = client.GetStream();
            State = ClientState.Active;
            new Thread(new ThreadStart(tReceive)).Start();
        }

        public void SendMessage(byte[] message)
        {
            new Thread(new ParameterizedThreadStart(tSendMessage)).Start(message);
        }

        private void tSendMessage(object message)
        {
            byte[] buffer = (byte[])message;
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }

        private void tReceive()
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            while(true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = stream.Read(buffer, 0, 4096);
                    observer.OnNotify(new MessageWrapper(MessageType.Normal, buffer, Id));
                }
                catch
                {
                    State = ClientState.Disconnected;
                    observer.OnNotify(new MessageWrapper(MessageType.Disconnect, null, Id));
                    break;
                }

                if (bytesRead == 0)
                {
                    State = ClientState.Disconnected;
                    observer.OnNotify(new MessageWrapper(MessageType.Disconnect, null, Id));
                    break;
                }
            }
        }
    }
}
