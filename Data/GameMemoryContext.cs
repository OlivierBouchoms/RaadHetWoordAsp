using System.Collections.Generic;
using Models;

namespace Data
{
    public class GameMemoryContext: IGameContext
    {
        public Game AddTeams(Game game, List<Team> teams)
        {
            return new Game(game.Maxscore, teams);
        }

        public Game AddWordlist(Game game, Wordlist wordlist)
        {
            game.AddWordlist(wordlist);
            return game;
        }
    }
}