using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashCore
{
    class MatchObserver
    {
        Match match;
        public MatchObserver(Match match)
        {
            this.match = match;
        }

        public void OnNotify(NotificationType type, Card card, ZoneType zone, Player player)
        {

            foreach (Player p in match.Players)
            {
                foreach (Card c in p.ZoneContainer.Zones[ZoneType.Play])
                {
                    switch (type)
                    {
                        case NotificationType.EnterZone:
                            c.OnCardEnter(zone, card);
                            break;
                        case NotificationType.ExitZone:
                            c.OnCardExit(zone, card);
                            break;
                        case NotificationType.BeginTurn:
                            if(c.Owner.Equals(player))
                            {
                                c.OnBeginTurn();
                            }
                            else
                            {
                                c.OnOpponentBeginTurn();
                            }
                            break;
                        case NotificationType.EndTurn:
                            if (c.Owner.Equals(player))
                            {
                                c.OnEndTurn();
                            }
                            else
                            {
                                c.OnOpponentEndTurn();
                            }
                            for (int i = 0; i < Global.NumPlayers; i++)
                            {
                                if(!match.Players[i].Equals(player))
                                {
                                    match.Players[i].BeginTurn();
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
