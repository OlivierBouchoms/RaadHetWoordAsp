using Models;

namespace Data
{
    public class TeamInGameRepository
    {
        private readonly ITeamInGameContext context;

        public TeamInGameRepository(ITeamInGameContext context)
        {
            this.context = context;
        }

        public Team IncreaseScore(Team team)
        {
            return context.IncreaseScore(team);
        }

        public Team DecreaseScore(Team team)
        {
            return context.DecreaseScore(team);
        }
    }
}
