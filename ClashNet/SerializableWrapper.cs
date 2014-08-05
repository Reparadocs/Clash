using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashCore;

namespace ClashNet
{
    public class SerializableWrapper
    {
        public int ClientId { get; private set; }
        public MessageType Type { get; private set; }
        public ISerializable Serializable { get; private set; }

        public SerializableWrapper(MessageType type, ISerializable serializable)
        {
            this.Type = type;
            this.Serializable = serializable;
        }

        public SerializableWrapper(MessageType type, ISerializable serializable, int clientId)
            : this(type, serializable)
        {
            this.ClientId = clientId;
        }
    }
}
