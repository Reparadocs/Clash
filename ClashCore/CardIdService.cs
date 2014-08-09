using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class CardIdService
    {
        Dictionary<Type, int> cardIdDict;
        Dictionary<int, Type> idCardDict;
        int currentId = 0;

        private void AddCards()
        {
            Add(typeof(Cards.Fastlane));
            Add(typeof(Cards.GridSurge));
            Add(typeof(Cards.HeatVision));
            Add(typeof(Cards.Helmet));
            Add(typeof(Cards.Voidling));
        }

        public CardIdService()
        {
            cardIdDict = new Dictionary<Type, int>();
            idCardDict = new Dictionary<int,Type>();
            AddCards();
        }

        private void Add(Type cardType)
        {
            currentId++;
            if (typeof(Card).IsAssignableFrom(cardType)) 
            {
                cardIdDict.Add(cardType, currentId);
                idCardDict.Add(currentId, cardType);
            }
        }

        public int GetID(Type card)
        {
            return cardIdDict[card];
        }

        public Type GetCard(int id)
        {
            return idCardDict[id];
        }
    }
}
