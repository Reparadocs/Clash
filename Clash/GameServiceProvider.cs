using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Clash
{
    static class GameServiceProvider
    {
        static GameServiceContainer services;

        public static void Initialize(GameServiceContainer gameServices)
        {
            services = gameServices;
            AddService<ClientHandlerService>(new ClientHandlerService());
        }

        public static void AddService<T>(T service)
        {
            services.AddService(typeof(T), service);
        }

        public static T GetService<T>()
        {
            return (T)services.GetService(typeof(T));
        }
    }
}
