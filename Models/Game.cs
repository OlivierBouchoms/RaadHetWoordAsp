using System.Collections.Generic;

namespace Models
{
    public class Game
    {
        public List<Team> TeamList { get; }
        public int Maxscore { get; }
        public Round CurrentRound { get; set; }
        public Wordlist Wordlist { get; private set; }

        public Game(int maxscore, List<Team> teamList)
        {
            Maxscore = maxscore;
            TeamList = teamList;
        }

        public void AddWordlist(Wordlist wordlist)
        {
            Wordlist = wordlist;
        }

    }
}
