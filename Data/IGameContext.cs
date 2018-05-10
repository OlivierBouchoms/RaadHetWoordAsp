using System;
using System.Collections.Generic;
using Models;

namespace Data
{
    public interface IGameContext
    {
        Game AddTeams(Game game, List<Team> teams);

        Game AddWordlist(Game game, Wordlist wordlist);

        Tuple<Game, int> ThrowDice(Game game, int maxValue);
    }
}
