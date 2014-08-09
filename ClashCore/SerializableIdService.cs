using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class SerializableIdService
    {
        private Dictionary<int, Type> serializableIdDict;
        private Dictionary<Type, int> reverseDict;
        private int currentId = 0;

        private void AddSerializables()
        {

        }

        public SerializableIdService()
        {
            serializableIdDict = new Dictionary<int,Type>();
            reverseDict = new Dictionary<Type, int>();
            AddSerializables();
        }

        private void Add(Type serializable)
        {
            if (typeof(ISerializable).IsAssignableFrom(serializable))
            {
                currentId++;
                serializableIdDict.Add(currentId, serializable);
                reverseDict.Add(serializable, currentId);
            }
            else
            {
                throw new Exception("Contians non-serializable types");
            }
            
        }

        public Dictionary<int, Type> getSerializableIdDict()
        {
            return serializableIdDict;
        }

        public Type getSerializable(int id)
        {
            return serializableIdDict[id];
        }

        public int getId(Type type)
        {
            return reverseDict[type];
        }
    }
}
