using Models;

namespace Data
{
    public class TeamInGameRepository
    {
        private readonly ITeamInGameContext _context;

        public TeamInGameRepository(ITeamInGameContext context)
        {
            _context = context;
        }

        public Team IncreaseScore(Team team)
        {
            return _context.IncreaseScore(team);
        }

        public Team DecreaseScore(Team team)
        {
            return _context.DecreaseScore(team);
        }

        public Team IncreaseTurns(Team team)
        {
            return _context.IncreaseTurns(team);
        }
    }
}
