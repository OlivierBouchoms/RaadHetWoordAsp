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

        public bool AddTeams(List<Team> teams, Game game)
        {
            return repo.AddTeams(teams, game);
        }

    }
}
