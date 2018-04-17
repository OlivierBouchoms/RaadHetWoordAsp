using Data;
using Models;

namespace Logic
{
    /// <summary>
    /// Contains all memory logic for Teams while ingame
    /// </summary>
    public class TeamInGameLogic
    {
        private readonly TeamInGameRepository repo;

        public TeamInGameLogic(TeamInGameRepository teamrepo)
        {
            repo = teamrepo;
        }
        
        public Team IncreaseScore(Team team)
        {
            return repo.IncreaseScore(team);
        }

        public Team DecreaseScore(Team team)
        {
            return repo.DecreaseScore(team);
        }

        public Team IncreaseTurns(Team team)
        {
            return repo.IncreaseTurns(team);
        }
    }
}
