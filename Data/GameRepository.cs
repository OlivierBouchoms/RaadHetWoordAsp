using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data
{
    public class GameRepository
    {
        private readonly IGameContext context;

        public GameRepository(IGameContext context)
        {
            this.context = context;
        }

        public Game AddTeams(List<Team> teams, Game game)
        {
            return context.AddTeams(game, teams);
        }

        public Game AddWordlist(Game game, Wordlist wordlist)
        {
            return context.AddWordlist(game, wordlist);
        }
    }
}
