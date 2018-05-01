using System.Collections.Generic;
using Models;

namespace Data
{
    public interface ITeamContext
    {
        bool CheckIfExists(Team team);

        bool AddTeam(Team team);

        List<Team> GetTeams();

        bool IncreaseScore(Team team);

        bool DecreaseScore(Team team);

        bool IncreaseTurns(Team team);

        bool IncreaseWins(Team team);

        bool IncreaseLosses(Team team);
    }
}