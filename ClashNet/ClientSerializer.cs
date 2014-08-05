using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ClashCore;
using System.IO;
using System.Net.Sockets;

namespace ClashNet
{
    public class ClientSerializer : IMessageObserver
    {
        SerializableIdService serializableIdService;
        Client client;
        ISerializableObserver observer;

        public ClientSerializer(Client client, ISerializableObserver observer)
        {
            this.client = client;
            this.observer = observer;
            serializableIdService = ServiceLocator.GetService<SerializableIdService>();
        }

        public void OnNotify(MessageWrapper message)
        {
            using (MemoryStream m = new MemoryStream(message.Message))
            {
                using (BinaryReader r = new BinaryReader(m))
                {
                    byte[] objMessage = new byte[4096];
                    Buffer.BlockCopy(message.Message, sizeof(Int32), objMessage, 0, message.Message.Length - sizeof(Int32));
                    ISerializable s = (ISerializable)serializableIdService.getSerializable(r.ReadInt32()).GetMethod("Deserialize").Invoke(null, new object[] { objMessage });
                    observer.OnNotify(new SerializableWrapper(message.Type, s));
                }
            }
        }

        public void SendSerializable(ISerializable serializable)
        {
            new Thread(new ParameterizedThreadStart(tSendSerializable)).Start(serializable);
        }

        private void tSendSerializable(object serializable)
        {
            ISerializable s = (ISerializable)serializable;
            byte[] typeId = BitConverter.GetBytes(serializableIdService.getId(s.GetType()));
            byte[] message = s.Serialize(s);
            byte[] final = new byte[typeId.Length + message.Length];
            Buffer.BlockCopy(typeId, 0, final, 0, typeId.Length);
            Buffer.BlockCopy(message, 0, final, typeId.Length, message.Length);
            client.SendMessage(final);
        }

    }
}
