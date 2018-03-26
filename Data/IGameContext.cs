using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data
{
    public interface IGameContext
    {
        bool AddTeams(List<Team> teams, Game game);
    }
}
