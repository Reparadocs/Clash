using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashNet
{
    public interface IConnectObserver
    {
        void OnNotify(int clientId);
    }
}
