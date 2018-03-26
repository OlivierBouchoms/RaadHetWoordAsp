using System;
using System.Collections.Generic;
using Models;

namespace Data
{
    public class GameMemoryContext: IGameContext
    {
        public bool AddTeams(List<Team> teams, Game game)
        {
            try
            {
                game.TeamList = teams;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}