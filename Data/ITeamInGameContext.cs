using Models;

namespace Data
{
    public interface ITeamInGameContext
    {
        Team IncreaseScore(Team team);

        Team DecreaseScore(Team team);
        Team IncreaseTurns(Team team);
    }
}
