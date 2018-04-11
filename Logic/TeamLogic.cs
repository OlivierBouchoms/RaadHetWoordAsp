using Data;
using Models;

namespace Logic
{
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

        
    }
}
