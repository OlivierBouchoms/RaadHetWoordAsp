using System;
using Data;
using Models;

namespace Logic
{
    /// <summary>
    /// Contains all database logic for Teams
    /// </summary>
    public class TeamLogic
    {
        private readonly TeamRepository repo;

        public TeamLogic(TeamRepository teamRepository)
        {
            repo = teamRepository;
        }

        private bool CheckIfExists(Team team)
        {
            try
            {
                return repo.CheckIfExists(team);
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
                return repo.AddTeam(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }

            return false;
        }

        public Team FillWithData(Team team)
        {
            try
            {
                return repo.FillWithData(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }

            return team;
        }

        public bool IncreaseScore(Team team)
        {
            try
            {
                return repo.IncreaseScore(team);
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
                return repo.DecreaseScore(team);
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
                return repo.InceaseTurns(team);
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
                return repo.IncreaseWins(team);
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
                return repo.IncreaseLosses(team);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
            return false;
        }
    }
}
