using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class SerializableIdService
    {
        List<Type> serializableTypes;
        Dictionary<int, Type> serializableIdDict;
        Dictionary<Type, int> reverseDict;

        public SerializableIdService()
        {
            serializableIdDict = new Dictionary<int,Type>();
            reverseDict = new Dictionary<Type, int>();
            serializableTypes = new List<Type>();
            //Add serializable types here WITH ADD

            int curId = 0;
            foreach(Type t in serializableTypes)
            {
                if (!typeof(ISerializable).IsAssignableFrom(t))
                {
                    throw new Exception("The list contains non-serializable types");
                }
                else
                {
                    serializableIdDict.Add(curId, t);
                    curId++;
                }
            }
        }

        private void Add(int id, Type serializable)
        {
            serializableIdDict.Add(id, serializable);
            reverseDict.Add(serializable, id);
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
