using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public static class ServiceLocator
    {
        static Dictionary<Type, object> services;

        public static void Initialize()
        {
            services = new Dictionary<Type, object>();
            AddService<SerializableIdService>(new SerializableIdService());
            AddService<CardIdService>(new CardIdService());
        }

        public static T GetService<T>()
        {
            try
            {
                return (T)services[typeof(T)];
            }
            catch
            {
                throw new Exception("Requested service not found");
            }
        }

        public static void AddService<T>(T service)
        {
            if(services.ContainsKey(typeof(T)))
            {
                throw new Exception("The service you are trying to add already exists");
            }
            else
            {
                services.Add(typeof(T), service);
            }
        }
    }
}
