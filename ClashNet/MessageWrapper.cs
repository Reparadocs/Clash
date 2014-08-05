using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashNet
{
    public class MessageWrapper
    {
        public int ClientId { get; private set; }
        public MessageType Type { get; private set; }
        public byte[] Message { get; private set; }

        public MessageWrapper(MessageType type, byte[] message)
        {
            this.Type = type;
            this.Message = message;
        }

        public MessageWrapper(MessageType type, byte[] message, int clientId)
            : this(type, message)
        {
            this.ClientId = clientId;
        }
    }
}
