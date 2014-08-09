using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ClashNet;
using ClashCore;

namespace ClashServer
{
    class Server : ISerializableObserver, IConnectObserver
    {
        private ListenerSerializer listener;
        private int waiting = -1;
        private List<MatchHandler> matches;

        public Server(int port)
        {
            matches = new List<MatchHandler>();
            new Thread(new ParameterizedThreadStart(tStartListener)).Start(port);
        }

        private void tStartListener(object port)
        {
            listener = new ListenerSerializer((int)port, this, this);
        }

        public void OnNotify(int clientId)
        {
            new Thread(new ParameterizedThreadStart(tNewClient)).Start(clientId);
        }

        private void tNewClient(object cId)
        {
            int clientId = (int)cId;
            if(waiting == -1)
            {
                waiting = clientId;
            }
            else
            {
                new Thread(new ParameterizedThreadStart(tNewGame)).Start(new int[2] { waiting, clientId });
            }
        }

        private void tNewGame(object idArr)
        {
            int[] playerIds = (int[])idArr;
            matches.Add(new MatchHandler(playerIds[0], playerIds[1]));
        }

        public void OnNotify(SerializableWrapper serializable)
        {
            foreach(MatchHandler mh in matches)
            {
                if(mh.PlayerIds.Contains(serializable.ClientId))
                {
                    mh.OnNotify(serializable);
                }
            }
            Console.WriteLine("No match found with client id " + serializable.ClientId);
        }
    }
}
