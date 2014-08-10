using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public interface ISerializable
    {
        //All implementation should also include a static Deserialize method
        byte[] Serialize();
    }
}
