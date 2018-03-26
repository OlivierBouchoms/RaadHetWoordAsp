using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Round
    {
        internal Team Team { get; set; }
        internal int Time { get; private set; }
        internal static int playerindex { get; private set; }

        public Round(Game game)
        {
            Team = game.TeamList[playerindex];
            if (playerindex == game.TeamList.Count - 1)
            {
                playerindex = 0;
            }
            else
            {
                playerindex++;
            }
        }
    }
}
