using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Round
    {
        public Team Team { get; set; }
        public int Time { get; set; }
        public static int playerindex { get; private set; }

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
