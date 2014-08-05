using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashNet;
using ClashCore;

namespace ClashServer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MessageWrapper> listeners = new List<MessageWrapper>();
            listeners.Add(new MessageWrapper(12, MessageType.Connect, new byte[4] { 0, 4, 0, 1 }));
            List<MessageWrapper> listenerCopy = listeners.ConvertAll(m => new MessageWrapper(m.ClientId, m.Type, m.Message));
            listeners.Clear();
            Console.WriteLine(listenerCopy[0].Message[1]);
            MessageWrapper wrap = listeners[0];
            while (true) {  }
        }
    }
}
