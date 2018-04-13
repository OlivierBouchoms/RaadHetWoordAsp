using System.Collections.Generic;

namespace Models
{
    public class Game
    {
        public List<Team> TeamList { get; set; }
        public int Maxscore { get; set; }
        public Round CurrentRound { get; set; }
        public Wordlist Wordlist { get; set; }

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
