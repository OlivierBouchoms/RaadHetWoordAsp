using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data
{
    public class GameRepository
    {
        private IGameContext context;

        public GameRepository(IGameContext context)
        {
            this.context = context;
        }

        public bool AddTeams(List<Team> teams, Game game)
        {
            return context.AddTeams(teams, game);
        }
    }
}
