using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashNet;
using ClashCore;

namespace ClashServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceLocator.Initialize();
            Server server = new Server(3000);
        }
    }
}
