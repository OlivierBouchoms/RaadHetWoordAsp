using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data
{
    public interface IGameContext
    {
        Game AddTeams(Game game, List<Team> teams);

        Game AddWordlist(Game game, Wordlist wordlist);
    }
}
