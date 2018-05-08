using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Models;

namespace Logic
{
    /// <summary>
    /// Contains all database logic for Teams
    /// </summary>
    public class TeamLogic
    {
        private readonly TeamRepository _repo;

        public TeamLogic(TeamRepository repo)
        {
            _repo = repo;
        }

        private bool CheckIfExists(Team team)
        {
            try
            {
                return _repo.CheckIfExists(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }

            return false;
        }

        public bool AddTeam(Team team)
        {
            if (CheckIfExists(team))
            {
                return false;
            }

            try
            {
                return _repo.AddTeam(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }

            return false;
        }

        public bool IncreaseScore(Team team)
        {
            try
            {
                return _repo.IncreaseScore(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }

            return false;
        }

        public bool DecreaseScore(Team team)
        {
            try
            {
                return _repo.DecreaseScore(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
            return false;
        }

        public bool IncreaseTurns(Team team)
        {
            try
            {
                return _repo.InceaseTurns(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
            return false;
        }

        public bool IncreaseWins(Team team)
        {
            try
            {
                return _repo.IncreaseWins(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
            return false;
        }

        public bool IncreaseLosses(Team team)
        {
            try
            {
                return _repo.IncreaseLosses(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
            return false;
        }

        public List<Team> GetTeams(string orderby)
        {
            if (orderby == null) orderby = "Score"; 
            try
            {
                var orderBy = (OrderBy)Enum.Parse(typeof(OrderBy), orderby);

                if (OrderBy.Wins == orderBy)
                {
                    return _repo.GetTeams().OrderByDescending(x => x.Wins).ToList();
                }
            
                if (OrderBy.WinLoss == orderBy)
                {
                    return _repo.GetTeams().OrderByDescending(x => x.WinLoss).ToList();
                }

                return _repo.GetTeams().OrderByDescending(x => x.Score).ToList();
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
            return new List<Team>();
        }

        public List<Team> GetTopTeams()
        {
            try
            {
                var teams = _repo.GetTeams().OrderByDescending(x => x.Score).ToList();
                if (teams.Count <= 10) return teams;
                teams.RemoveRange(10, teams.Count - 10);
                return teams;
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
            return new List<Team>();
        }
    }
}
