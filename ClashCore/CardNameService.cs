using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    public class CardNameService
    {
        private Dictionary<string, Type> nameCardDict;

        private void AddCards()
        {
            Add(typeof(Cards.Fastlane), "Fastlane");
            Add(typeof(Cards.GridSurge), "Grid Surge");
            Add(typeof(Cards.HeatVision), "Heat Vision");
            Add(typeof(Cards.Helmet), "Helmet");
            Add(typeof(Cards.Voidling), "Voidling");
        }

        public CardNameService()
        {
            nameCardDict = new Dictionary<string, Type>();
            AddCards();
        }

        private void Add(Type card, string name)
        {
            if(typeof(Card).IsAssignableFrom(card))
            {
                nameCardDict.Add(name, card);
            }
            else
            {
                throw new Exception("Contains non-card types");
            }
        }

        public Type GetCardType(string name)
        {
            return nameCardDict[name];
        }

        public Card GetNewCardInstance(string name, Player owner)
        {
            Type cardType = GetCardType(name);
            return (Card)Activator.CreateInstance(cardType, new object[] {owner});
        }
    }
}
