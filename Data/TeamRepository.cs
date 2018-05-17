using System.Collections.Generic;
using Models;

namespace Data
{
    public class TeamRepository
    {
        private readonly ITeamContext _context;

        public TeamRepository(ITeamContext context)
        {
            _context = context;
        }

        public bool CheckIfExists(Team team)
        {
            return _context.CheckIfExists(team);
        }

        public bool AddTeam(Team team)
        {
            return _context.AddTeam(team);
        }

        public bool IncreaseScore(Team team)
        {
            return _context.IncreaseScore(team);
        }

        public bool DecreaseScore(Team team)
        {
            return _context.DecreaseScore(team);
        }

        public bool InceaseTurns(Team team)
        {
            return _context.IncreaseTurns(team);
        }

        public bool IncreaseWins(Team team)
        {
            return _context.IncreaseWins(team);
        }

        public bool IncreaseLosses(Team team)
        {
            return _context.IncreaseLosses(team);
        }

        public List<Team> GetTeams()
        {
            return _context.GetTeams();
        }

        public Team GetTeam(int id)
        {
            return _context.GetTeam(id);
        }
    }
}