using System;
using System.Collections.Generic;

namespace Models
{
    public class Game
    {
        public List<Team> TeamList;
        public int Maxscore { get; }
        public Round CurrentRound { get; set; }
        public Wordlist Wordlist { get; set; }

        public Game(int maxscore, List<Team> teamList)
        {
            Maxscore = maxscore;
            TeamList = teamList;
        }

    }
}
