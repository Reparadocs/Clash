using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using ClashCore;

namespace ClashNet
{
    public class ListenerSerializer : Listener
    {
        private class ClientIdSerializableWrapper
        {
            public int clientId { get; private set; }
            public ISerializable serializable { get; private set; }

            public ClientIdSerializableWrapper(int clientId, ISerializable serializable)
            {
                this.clientId = clientId;
                this.serializable = serializable;
            }
        }

        SerializableIdService serializableIdService;

        public ListenerSerializer(int port)
            : base(port)
        {
            serializableIdService = ServiceLocator.GetService<SerializableIdService>();
        }

        public List<SerializableWrapper> GetSerializables()
        {
            List<SerializableWrapper> serializables = new List<SerializableWrapper>();
            List<MessageWrapper> messages = GetMessages();
            foreach (MessageWrapper message in messages)
            {
                using (MemoryStream m = new MemoryStream(message.Message))
                {
                    using (BinaryReader r = new BinaryReader(m))
                    {
                        byte[] objMessage = new byte[4096];
                        Buffer.BlockCopy(message.Message, sizeof(Int32), objMessage, 0, message.Message.Length - sizeof(Int32));
                        ISerializable s = (ISerializable)serializableIdService.getSerializable(r.ReadInt32()).GetMethod("Deserialize").Invoke(null, new object[] {objMessage});
                        serializables.Add(new SerializableWrapper(message.ClientId, message.Type, s));
                    }
                }
            }
            return serializables;
        }


        public void SendSerializableToClient(int clientId, ISerializable serializable)
        {
            new Thread(new ParameterizedThreadStart(tSendSerializableToClient)).Start(new ClientIdSerializableWrapper(clientId, serializable));
        }

        private void tSendSerializableToClient(object clientIdSerializable)
        {
            ClientIdSerializableWrapper wrapper = (ClientIdSerializableWrapper)clientIdSerializable;
            byte[] typeId = BitConverter.GetBytes(serializableIdService.getId(wrapper.serializable.GetType()));
            byte[] message = wrapper.serializable.Serialize(wrapper.serializable);
            byte[] final = new byte[typeId.Length + message.Length];
            Buffer.BlockCopy(typeId, 0, final, 0, typeId.Length);
            Buffer.BlockCopy(message, 0, final, typeId.Length, message.Length);
            SendMessageToClient(wrapper.clientId, final);
        }


    }
}
