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
    public class ListenerSerializer : IMessageObserver
    {
        public Listener Listener { get; private set; }

        SerializableIdService serializableIdService;
        ISerializableObserver observer;

        public ListenerSerializer(int port, ISerializableObserver observer)
        {
            this.Listener = new Listener(port, this);
            this.observer = observer;
            serializableIdService = ServiceLocator.GetService<SerializableIdService>();
        }

        public void OnNotify(MessageWrapper message)
        {
            new Thread(new ParameterizedThreadStart(tOnNotify)).Start(message);
        }

        private void tOnNotify(object message)
        {
            MessageWrapper wrapper = (MessageWrapper)message;
            using (MemoryStream m = new MemoryStream(wrapper.Message))
            {
                using (BinaryReader r = new BinaryReader(m))
                {
                    byte[] objMessage = new byte[4096];
                    Buffer.BlockCopy(wrapper.Message, sizeof(Int32), objMessage, 0, wrapper.Message.Length - sizeof(Int32));
                    ISerializable s = (ISerializable)serializableIdService.getSerializable(r.ReadInt32()).GetMethod("Deserialize").Invoke(null, new object[] { objMessage });
                    observer.OnNotify(new SerializableWrapper(wrapper.Type, s, wrapper.ClientId));
                }
            }

        }

        public void SendSerializableToClient(ISerializable serializable, int clientId)
        {
            new Thread(new ParameterizedThreadStart(tSendSerializableToClient)).Start(new SerializableWrapper(serializable, clientId));
        }

        private void tSendSerializableToClient(object clientIdSerializable)
        {
            SerializableWrapper wrapper = (SerializableWrapper)clientIdSerializable;
            byte[] typeId = BitConverter.GetBytes(serializableIdService.getId(wrapper.Serializable.GetType()));
            byte[] message = wrapper.Serializable.Serialize(wrapper.Serializable);
            byte[] final = new byte[typeId.Length + message.Length];
            Buffer.BlockCopy(typeId, 0, final, 0, typeId.Length);
            Buffer.BlockCopy(message, 0, final, typeId.Length, message.Length);
            Listener.SendMessageToClient(final, wrapper.ClientId);
        }


    }
}
