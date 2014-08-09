using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClashCore.Serializables
{
    public class Card : ISerializable
    {
        public Type TargetCard { get; private set; }
        public CardActions Action { get; private set; }

        private CardIdService idService;

        public Card(Type targetCard, CardActions action)
        {
            if(typeof(Card).IsAssignableFrom(targetCard))
            {
                this.TargetCard = targetCard;
                this.Action = action;
            }
            else
            {
                throw new Exception("Not a card");
            }
            idService = ServiceLocator.GetService<CardIdService>();
        }

        public byte[] Serialize()
        {
            using(MemoryStream m = new MemoryStream())
            {
                using(BinaryWriter b = new BinaryWriter(m))
                {
                    b.Write(idService.GetID(TargetCard));
                    b.Write((int)Action);
                }
                return m.ToArray();
            }
        }

        public ISerializable Deserialize(byte[] buffer)
        {
            using (MemoryStream m = new MemoryStream(buffer))
            {
                using(BinaryReader b = new BinaryReader(m))
                {
                    Type t = idService.GetCard(b.ReadInt32());
                    CardActions c = (CardActions)b.ReadInt32();
                    return new Card(t, c);
                }
            }
        }

    }
}
