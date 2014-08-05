using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public interface ISerializable
    {
        byte[] Serialize(ISerializable obj);
        ISerializable Deserialize(byte[] buffer);
    }
}
