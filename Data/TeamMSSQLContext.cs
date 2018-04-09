using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace Data
{
    public class TeamMSSQLContext : ITeamContext
    {
        /// <summary>
        /// Checks if a team already exists in the database
        /// </summary>
        public bool CheckIfExists(Team team)
        {
            var sqlConnection = DataBase._SqlConn;
            try
            {
                sqlConnection.Open();
            }
            catch
            {
                return false;
            }

            var commandText = "Select [Name] From [Team] where [Name] =@name";
            var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.Add("name", SqlDbType.NVarChar).Value = team.Name;
            var sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                sqlConnection.Close();
                return true;
            }
            sqlConnection.Close();
            return false;
        }

        /// <summary>
        /// Adds a team to the database
        /// </summary>
        public bool AddTeam(Team team)
        {
            var sqlConnection = DataBase._SqlConn;
            try
            {
                sqlConnection.Open();
            }
            catch
            {
                return false;
            }

            var commandText = "Insert into Team ([Name], [Score], [Turns], [Wins], [Losses]) " +
                              "Values @name, 0, 0, 0, 0)";
            var sqlCommand = new SqlCommand(commandText, sqlConnection);
            if (sqlCommand.ExecuteNonQuery() == 1)
            {
                sqlConnection.Close();
                return true;
            }
            sqlConnection.Close();
            return false;
        }

        public Team FillWithData(Team team)
        {
            var sqlConnection = DataBase._SqlConn;
            try
            {
                sqlConnection.Open();
            }
            catch
            {
                return team;
            }

            var commandText = "SELECT * From [Team] where [Name] = @name";
            var sqlCommand = new SqlCommand(commandText, sqlConnection);
            sqlCommand.Parameters.Add("name", SqlDbType.NVarChar).Value = team.Name;
            var sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (sqlDataReader.HasRows)
                {
                    var _team = new Team(team.Name, sqlDataReader.GetInt32(2), sqlDataReader.GetInt32(3), sqlDataReader.GetInt32(4), sqlDataReader.GetInt32(5), sqlDataReader.GetDecimal(6), sqlDataReader.GetDecimal(7));
                    sqlDataReader.Close();
                    return _team;
                }
            }
            return team;
        }

        /// <summary>
        /// Get all teams sorted by alpabet
        /// </summary>
        public List<Team> GetTeamsByAlphabet()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all teams sorted by score
        /// </summary>
        public List<Team> GetTeamsByScore()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all teams sorted by turns
        /// </summary>
        public List<Team> GetTeamByTurns()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all teams sorted by wins
        /// </summary>
        public List<Team> GetTeamsByWins()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all teams sorted by winloss
        /// </summary>
        public List<Team> GetTeamsByWinLoss()
        {
            throw new NotImplementedException();
        }
    }
}
