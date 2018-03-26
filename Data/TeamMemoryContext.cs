using System.Collections.Generic;
using Models;

namespace Data
{
    public class TeamMemoryContext : ITeamContext
    {
        public bool CheckIfExists(Team team)
        {
            throw new System.NotImplementedException();
        }

        public bool AddTeam(Team team)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteTeam(string name)
        {
            throw new System.NotImplementedException();
        }

        public List<Team> GetTeamsByAlphabet()
        {
            throw new System.NotImplementedException();
        }

        public List<Team> GetTeamsByScore()
        {
            throw new System.NotImplementedException();
        }

        public List<Team> GetTeamByTurns()
        {
            throw new System.NotImplementedException();
        }

        public List<Team> GetTeamsByWins()
        {
            throw new System.NotImplementedException();
        }

        public List<Team> GetTeamsByWinLoss()
        {
            throw new System.NotImplementedException();
        }
    }
}