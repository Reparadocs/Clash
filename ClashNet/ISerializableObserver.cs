using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashCore;

namespace ClashNet
{
    public interface ISerializableObserver
    {
        void OnNotify(SerializableWrapper serializable);
    }
}
