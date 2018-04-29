using System.Collections.Generic;
using Models;

namespace Data
{
    public class GameRepository
    {
        private readonly IGameContext _context;

        public GameRepository(IGameContext context)
        {
            _context = context;
        }

        public Game AddTeams(List<Team> teams, Game game)
        {
            return _context.AddTeams(game, teams);
        }

        public Game AddWordlist(Game game, Wordlist wordlist)
        {
            return _context.AddWordlist(game, wordlist);
        }
    }
}
