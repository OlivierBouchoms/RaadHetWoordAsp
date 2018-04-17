using System.Collections.Generic;
using Models;

namespace Data
{
    public interface ITeamContext
    {
        bool CheckIfExists(Team team);

        bool AddTeam(Team team);

        List<Team> GetTeamsByAlphabet();

        List<Team> GetTeamsByScore();

        List<Team> GetTeamByTurns();

        List<Team> GetTeamsByWins();

        List<Team> GetTeamsByWinLoss();

        Team FillWithData(Team team);

        bool IncreaseScore(Team team);

        bool DecreaseScore(Team team);

        bool IncreaseTurns(Team team);
    }
}