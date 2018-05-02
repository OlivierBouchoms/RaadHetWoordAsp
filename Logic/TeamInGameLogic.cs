using Data;
using Models;

namespace Logic
{
    /// <summary>
    /// Contains all logic for Teams while ingame
    /// </summary>
    public class TeamInGameLogic
    {
        private readonly TeamInGameRepository _repo;

        public TeamInGameLogic(TeamInGameRepository repo)
        {
            _repo = repo;
        }
        
        public Team IncreaseScore(Team team)
        {
            return _repo.IncreaseScore(team);
        }

        public Team DecreaseScore(Team team)
        {
            return _repo.DecreaseScore(team);
        }

        public Team IncreaseTurns(Team team)
        {
            return _repo.IncreaseTurns(team);
        }
    }
}
