using System.Collections.Generic;
using Models;

namespace Data
{
    public interface ITeamContext
    {
        //Moet kunnen kijken of team bestaat
        //Moet een team kunnen toevoegen
        //Moet een team kunnen verwijderen

        bool CheckIfExists(Team team);

        bool AddTeam(Team team);

        List<Team> GetTeamsByAlphabet();

        List<Team> GetTeamsByScore();

        List<Team> GetTeamByTurns();

        List<Team> GetTeamsByWins();

        List<Team> GetTeamsByWinLoss();

        Team FillWithData(Team team);
    }
}