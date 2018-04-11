using Models;

namespace Data
{
    public class TeamInGameMemoryContext : ITeamInGameContext 
    {
        public Team IncreaseScore(Team team)
        {
            team.IncreaseScore();
            return team;
        }

        public Team DecreaseScore(Team team)
        {
            team.DecreaseScore();
            return team;
        }
    }
}
