using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClashCore.Serializables
{
    public class CardSerializer : ISerializable
    {
        public string TargetCard { get; private set; }
        public CardActions Action { get; private set; }

        public CardSerializer(string targetCard, CardActions action)
        {
            this.TargetCard = targetCard;
            this.Action = action;
        }

        public override byte[] Serialize()
        {
            using(MemoryStream m = new MemoryStream())
            {
                using(BinaryWriter b = new BinaryWriter(m))
                {
                    b.Write(TargetCard);
                    b.Write((int)Action);
                }
                return m.ToArray();
            }
        }

        public static ISerializable Deserialize(byte[] buffer)
        {
            using (MemoryStream m = new MemoryStream(buffer))
            {
                using(BinaryReader b = new BinaryReader(m))
                {
                    string s = b.ReadString();
                    CardActions c = (CardActions)b.ReadInt32();
                    return new CardSerializer(s, c);
                }
            }
        }

    }
}
