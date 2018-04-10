using System;
using System.Collections.Generic;
using Data;
using Models;

namespace Logic
{
    public class GameLogic
    {
        private GameRepository repo;

        public GameLogic(GameRepository gameRepository)
        {
            repo = gameRepository;
        }

        public Game AddTeams(List<Team> teams, Game game)
        {
            return repo.AddTeams(teams, game);
        }

        public Game AddWordlist(Game game, Wordlist wordlist)
        {
            return repo.AddWordlist(game, wordlist);
        }

    }
}
