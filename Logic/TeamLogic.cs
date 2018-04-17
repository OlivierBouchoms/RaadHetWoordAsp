using Data;
using Models;

namespace Logic
{
    /// <summary>
    /// Contains all database logic for Teams
    /// </summary>
    public class TeamLogic
    {
        private readonly TeamRepository repo;

        public TeamLogic(TeamRepository teamRepository)
        {
            repo = teamRepository;
        }

        public bool CheckIfExists(Team team)
        {
            return repo.CheckIfExists(team);
        }

        public bool AddTeam(Team team)
        {
            if (CheckIfExists(team))
            {
                return false;
            }

            return repo.AddTeam(team);
        }

        public Team FillWithData(Team team)
        {
            return repo.FillWithData(team);
        }

        public bool IncreaseScore(Team team)
        {
            return repo.IncreaseScore(team);
        }

        public bool DecreaseScore(Team team)
        {
            return repo.DecreaseScore(team);
        }

        public bool IncreaseTurns(Team team)
        {
            return repo.InceaseTurns(team);
        }
    }
}
