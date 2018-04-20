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

        private bool CheckIfExists(Team team)
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

        public bool IncreaseWins(Team team)
        {
            return repo.IncreaseWins(team);
        }

        public bool IncreaseLosses(Team team)
        {
            return repo.IncreaseLosses(team);
        }
    }
}
