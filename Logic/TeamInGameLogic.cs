using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class TeamInGameLogic
    {
        private readonly TeamInGameRepository repo;

        public TeamInGameLogic(TeamInGameRepository repo)
        {
            this.repo = repo;
        }
        
        public Team IncreaseScore(Team team)
        {
            return repo.IncreaseScore(team);
        }

        public Team DecreaseScore(Team team)
        {
            return repo.DecreaseScore(team);
        }
    }
}
